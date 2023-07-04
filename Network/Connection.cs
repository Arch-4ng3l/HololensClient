using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditorInternal;
using System.Text;

public class Connection : MonoBehaviour
{
    public GameObject Player;
    private CostumTcpClient client;
    float x = 0; 
    float y = 0;
    float z = 0;
    public string ip; 
    public int port;
    // Start is called before the first frame update
    void Start()
    {
        Console.WriteLine("Starting Connection");
        client = new CostumTcpClient();
        client.NewCostumTcpClient(ip, port);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SendPosition(GameObject obj)
    {
        GetPos(obj);
        byte[] data = ConvertToBytes(obj.name);
        client.WriteToStream(data, data.Length);
    }
    private byte[] ConvertToBytes(string nameOfObject)
    {
        float[] data = new float[3];
        byte[] dataBytes = new byte[2096];
        for(int i = 0; i < dataBytes.Length; i++)
        {
            dataBytes[i] = (byte)'\0';
        }
        int size = sizeof(float);
        data[0] = x;
        data[1] = y;
        data[2] = z;

        for (int i = 0; i < data.Length; i++)
        {

            Buffer.BlockCopy(data, i * size, dataBytes, i * size, sizeof(float));
        }

        Buffer.BlockCopy(Encoding.ASCII.GetBytes(nameOfObject), 0, dataBytes, 3 * size, Encoding.ASCII.GetBytes(nameOfObject).Length);
        return dataBytes;
 
    }
    private void GetPos(GameObject obj)
    {
        x = obj.transform.position.x;
        y = obj.transform.position.y;
        z = obj.transform.position.z;
    
    }
}
