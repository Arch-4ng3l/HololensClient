using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditorInternal;

public class Connection : MonoBehaviour
{
    public GameObject Player;
    private CostumTcpClient client;
    float x = 0; 
    float y = 0;
    float z = 0;
    // Start is called before the first frame update
    void Start()
    {
        Console.WriteLine("Starting Connection");
        client = new CostumTcpClient();
        client.NewCostumTcpClient("192.168.45.163", 3000);
        GetPos();
        client.ReadFromStream();
    }

    // Update is called once per frame
    void Update()
    {
        GetPos();
    }

    public void SendPosition()
    {
        client.ReadFromStream();
        byte[] position = ConvertPositionToBytes();
        client.WriteToStream(position, position.Length);

    }
    private byte[] ConvertPositionToBytes()
    {
        float[] data = new float[3];
        byte[] dataBytes = new byte[1024];
        int size = sizeof(float);
        data[0] = x;
        data[1] = y;
        data[2] = z;    
        
        for(int i = 0; i < data.Length; i++)
        {
            
            Buffer.BlockCopy(data, i * size, dataBytes, i * size, sizeof(float));
        }
        return dataBytes;
 
    }
    private void GetPos()
    {
        x = Player.transform.position.x;
        y = Player.transform.position.y;
        z = Player.transform.position.z;
    
    }
}
