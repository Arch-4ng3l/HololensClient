using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int delay = 320;
    GameObject network; 
    // Start is called before the first frame update
    void Start()
    {

    network = GameObject.FindGameObjectsWithTag("NetworkManager")[0];
    }

    // Update is called once per frame
    void Update()
    {
        delay--;
        if (delay <= 0)
        {
            network.SendMessage("SendPosition", gameObject);
            delay = 320; 
        }
    }
}
