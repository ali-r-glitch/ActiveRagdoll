using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainFoces : MonoBehaviour
{
    [SerializeField] private Rigidbody Rightarrm;
    [SerializeField] private Rigidbody Leftarrm;
    [SerializeField] private Rigidbody rightleg;
    [SerializeField] private Rigidbody leftleg;
    [SerializeField] private Rigidbody torso;
    [SerializeField] private Rigidbody head;
    [SerializeField] private bool _bleftarm;
    [SerializeField]private bool _brightarm;
    private float temphandforce=40;
    private float tempupforce;
    private float switches=0;

    public float upforce;
    public float downforce;

    public float handmovement;
    // Start is called before the first frame update
    void Start()
    {
       
        tempupforce=  upforce-temphandforce;
    }

    // Update is called once per frame
    void Update()
    {
        head.AddForce(transform.up * upforce);
        
     
        torso.AddForce(-transform.up * downforce);
        if (_brightarm)
        {
            Rightarrm.AddForce(transform.up * handmovement);
        }
        if (_bleftarm)
        {
            Leftarrm.AddForce(transform.up * handmovement);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_bleftarm)
            {
               
                Movearm(Leftarrm);
                 _bleftarm = false;
            }
            

            
            _brightarm=!_brightarm;
            Movearm(Rightarrm);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_brightarm)
            {
                Movearm(Leftarrm);
                _brightarm = false; 
            }

           
            _bleftarm=!_bleftarm;
            Movearm(Leftarrm);
        }
    }

    private void Movearm(Rigidbody arm)
    {

        switches = handmovement;
        handmovement = temphandforce;
        temphandforce = switches;
        
        
        switches = upforce;
        upforce = tempupforce;
        tempupforce = switches;

    }

    public void distributedownforce()
    {
        
        //ill need to find a way to distribute the force that is apploed to the feet
        //figure out a ration for when walking.
        
    }
}
