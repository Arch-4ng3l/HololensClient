using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditorInternal;
using System.Text;
using System.Linq.Expressions;
using System.Threading;

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
        Thread.Sleep(10);
        client.WriteToStream(data, data.Length);


    }
    public void RecvObjects()
    {

        byte[] recv = new byte[4096];
        client.ReadFromStream(recv);
        string[] data = BytesToData(recv);
        for(int i = 0; i < data.Length; i++)
        {
            Debug.Log("Text: " + data[i]);
        } 
    }
    private string[] BytesToData(byte[] bytes)
    {
        string[] data = new string[0];
        int objectSize = 64;
        float[] pos = new float[3];
        ArraySegment<byte> tempSeg;
        for (int i = 0; i < bytes.Length; i+= objectSize) 
        {
            var seg = new ArraySegment<byte>(bytes, i, i + objectSize);
            for (int j = 0; j < 3; j++)
            {
                tempSeg = new ArraySegment<byte>(seg.Array, j * 4, j * 4 + 4);
                pos[j] = BitConverter.ToSingle(tempSeg.Array, 0);
            }
            tempSeg = new ArraySegment<byte>(seg.Array, 12, objectSize - 12);
            string name = BitConverter.ToString(tempSeg.Array);
            name = name.Replace("\x00", "");
            string obj = String.Format("x: %f, y: %f, z: %f, name: %s", pos[0], pos[1], pos[2], name);
            data.Append(obj);
        }
        return data;
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
