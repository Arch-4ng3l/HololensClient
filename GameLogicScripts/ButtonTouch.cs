using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonTouch : MonoBehaviour
{

    GameObject gameObject = null;
    List<GameObject> gameObjects = new List<GameObject>();
    int count = 0; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //textLabel.text = count.ToString();
    }

    void Front()
    {
        if (gameObject != null)
        {
            gameObject.transform.Translate(1, 0, 0);
        }
    }

    void Back()
    {
        if (gameObject != null)
        {
            gameObject.transform.Translate(-1, 0, 0);
        }
    }
    
    void DeleteLastObject()
    {
        if (gameObjects.Count > 0)
        {
            Destroy(gameObjects.Last());
            gameObjects.Remove(gameObjects.Last());
            count--;
        }
    }
    void SpawnObject(string type)
    {

        switch (type) {
            case "Cube":
                gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case "Sphere":
                gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            case "Capsula":
                gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                break;
            case "Cylinder":
                gameObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                break;
            default:
                return;
        }


        GameObject[] temp = GameObject.FindGameObjectsWithTag("MainCamera");

        Vector3 position = temp[0].transform.position;

        gameObject.transform.position = position;

        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        gameObject.AddComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>();

        gameObject.AddComponent<Rigidbody>();

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        rb.useGravity = false;

        name = gameObject.name + count.ToString();

        gameObject.name = name;
        gameObjects.Add(gameObject);
        count++;
    }
    void SpawnCube()
    {
        SpawnObject("Cube");
    }
    void SpawnSphere()
    {
        SpawnObject("Sphere");
    }
    void SpawnCylinder()
    {
        SpawnObject("Cylinder");
    }

    void SpawnCapsula()
    {
        SpawnObject("Capsula");
    }

}
