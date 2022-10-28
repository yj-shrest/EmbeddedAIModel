import numpy as np
import pickle
import socketserver
import socket
import os

class MyTCPHandler(socketserver.BaseRequestHandler):


    def handle(self):
        modelpath = os.getcwd() +"\\regressor.pkl"
        model = pickle.load(open(modelpath,'rb'))
        self.data = self.request.recv(1024).strip()
        #print ("{} wrote:".format(self.client_address[0]))
        data = self.data.decode();
        #print (data)
        
        exp = int(data)
        prediction = model.predict([[np.array(exp)]])
        output = str(prediction[0])
       
        self.request.sendall(bytes(output,'utf-8'))
        

if __name__ == "__main__":
    HOST, PORT = "127.0.0.1", 11000

    server = socketserver.TCPServer((HOST, PORT), MyTCPHandler)
    server.serve_forever()