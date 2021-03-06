﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPClient : MonoBehaviour
{

    Thread receiveThread;

    UdpClient client;
    public string serverIP;
    public int serverPort;
    public int clientPort;

    private bool isThreadAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        client = new UdpClient(clientPort);
        receiveThread = new Thread(new ThreadStart(receiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        string text = "conectando";
        byte[] data = Encoding.UTF8.GetBytes(text);

        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
        client.Send(data, data.Length, remoteEndPoint);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnApplicationQuit()
    {
        isThreadAlive = false;
        // receiveThread.Abort();
    }

    public void receiveData()
    {
        while (isThreadAlive)
        {

            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);

                string text = Encoding.UTF8.GetString(data);

                print(">> " + text);



            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    public void sendData()
    {

    }
}
