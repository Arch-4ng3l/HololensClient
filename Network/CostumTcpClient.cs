using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Net;
using System.Threading;

public class CostumTcpClient
{
    const int bufferSize = 1024;


    private TcpClient socket;
    private NetworkStream stream;
    public Byte[] receiveBuffer;
    private Byte[] dataToSend;
    private int bytesLeft;

    private int port;
    private IPAddress ip;


    public void NewCostumTcpClient(string i, int p)
    {
        socket = new TcpClient
        {
            ReceiveBufferSize = bufferSize,
            SendBufferSize = bufferSize,
        };
        ip = IPAddress.Parse(i);
        port = p;
        ConnectionLoop(1);
        stream = socket.GetStream();
    }

    public void ConnectionLoop(int i)
    {

        if (!socket.Connected)
        {
            Thread.Sleep(i);
            i++;
            if (i > 12) return;
            try
            {
                socket.Connect(ip, port);
            }
            catch (SocketException)
            {
                ConnectionLoop(i);
            }
        }
        return; 
    }

    public void WriteToStream(byte[] data, int size)
    {
        dataToSend = data;
        bytesLeft = size;
        WriteConnectCallback();
    }

    public void EndConnection()
    {
        socket.Close();
    }

    private void WriteConnectCallback()
    {
        
        if (!socket.Connected)
        {
            UnityEngine.Debug.LogWarning(message: "Connection Closed");
            return;
        }
        stream = socket.GetStream();
        stream.Write(dataToSend, 0, bytesLeft);   
    }



    public void ReadFromStream(byte[] buffer)
    {
        stream = socket.GetStream();
        stream.Read(buffer, 0, 4);
        int len = BitConverter.ToInt32(buffer, 0);
        stream.Read(buffer, 0, len);
    }
}
