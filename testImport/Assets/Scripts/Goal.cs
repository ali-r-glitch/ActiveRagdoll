using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
   
    public GameObject ball;

    public Transform ballspawn;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ball")
        {
            Destroy(other.gameObject);
            Instantiate(ball, ballspawn.position, ballspawn.rotation);
        }
    }
}
