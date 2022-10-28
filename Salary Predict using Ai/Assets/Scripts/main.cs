using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;
public class main : MonoBehaviour
{   public TextMeshProUGUI output;
    string input;
    string salary;
    const int PORT_NO = 11000;
    const string SERVER_IP = "127.0.0.1";
    static string exp;
    static byte[] SendBytes;
    static byte[] ReadBytes;
    static int bytesRead;
    int sal;



    void Start()
    {   
        string path = Directory.GetCurrentDirectory();
        UnityEngine.Debug.Log(path);
        var p = new Process
                {
                    StartInfo =
                    {
                        FileName = "python",                        
                        WorkingDirectory = path,
                        Arguments = "server.py",
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                }.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    
    public void ReadInput(string str)
    {
        input = str;
        UnityEngine.Debug.Log(input);
    }

    public void predict(){
        TcpClient client = new TcpClient(SERVER_IP, PORT_NO);

        NetworkStream nwStream = client.GetStream();
        SendBytes = ASCIIEncoding.ASCII.GetBytes(input);
        nwStream.Write(SendBytes, 0, SendBytes.Length);

        ReadBytes = new byte[client.ReceiveBufferSize];
        bytesRead = nwStream.Read(ReadBytes, 0, client.ReceiveBufferSize);
        salary = Encoding.ASCII.GetString(ReadBytes, 0, bytesRead);
        output.text="The predicted salary for " + input +" years of experience is Rs. " + (int)Convert.ToDouble(salary);
        client.Close();
    }
}
