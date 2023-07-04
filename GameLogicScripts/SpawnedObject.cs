using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject network;
    Vector3 lastPosition = new Vector3();
    void Start()
    {
        network = GameObject.FindGameObjectsWithTag("NetworkManager")[0];
        lastPosition = transform.position;  
    }

    // Update is called once per frame
    void Update()
    {
        if(lastPosition != transform.position)
        {
            lastPosition = transform.position;
            network.SendMessage("SendPosition", gameObject);
        } 
    }



}
