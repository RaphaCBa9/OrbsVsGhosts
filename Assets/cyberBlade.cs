using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cyberBlade : MonoBehaviour
{
    public string targetTag = "Ghost";

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshCollider>().isTrigger = true; // Set the MeshCollider to be a trigger


    }


    // Update is called once per frame
    void Update()
    {
        


    }

    // Function to set the target object

}
