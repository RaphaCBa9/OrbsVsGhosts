using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliceableBySword : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blade"))
        {
            Debug.LogError("Sliced by the sword!");
            
            
            // destroy its own parent
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
