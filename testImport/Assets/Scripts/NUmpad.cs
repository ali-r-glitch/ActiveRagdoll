using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NUmpad : MonoBehaviour
{
    // Start is called before the first frame update
    public int number;
    public numpadmanagaer numpadmanagaer;
    private void OnTriggerEnter(Collider other)
    {
        numpadmanagaer.addnumber(number);
    }
}
