using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainFoces : MonoBehaviour
{
    [SerializeField] private Rigidbody Rightarrm;
    [SerializeField] private Rigidbody Leftarrm;
    [SerializeField] private Rigidbody rightleg;
    [SerializeField] private Rigidbody rightfoot;
    [SerializeField] private Rigidbody leftleg;
    [SerializeField] private Rigidbody torso;
    [SerializeField] private Rigidbody head;
    [SerializeField] private bool _bleftarm;
    [SerializeField] private bool _brightarm;

    private float temphandforce = 0.5f;
    private float tempupforce;
    private float switches = 0;
    private float baseDownforce;

    public float upforce;
    public float downforce;
    public float legforce;
    public float shinforces;
    public float handmovement;

    void Start()
    {
        tempupforce = upforce - temphandforce;
        baseDownforce = downforce; 
    }

    void Update()
    {
        ApplyForces();

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_bleftarm)
            {
                Movearm(Leftarrm);
                _bleftarm = false;
            }
            _brightarm = !_brightarm;
            Movearm(Rightarrm);
            UpdateDownforce();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_brightarm)
            {
                Movearm(Rightarrm);
                _brightarm = false;
            }
            _bleftarm = !_bleftarm;
            Movearm(Leftarrm);
            UpdateDownforce();
        }
    }

    void ApplyForces()
    {
        rightleg.AddForce(transform.up * legforce, ForceMode.Acceleration);
        rightfoot.AddForce(-transform.up * shinforces, ForceMode.Acceleration);
        head.AddForce(transform.up * upforce, ForceMode.Acceleration);
        torso.AddForce(-transform.up * downforce, ForceMode.Acceleration);

        if (_brightarm)
        {
            Rightarrm.AddForce(transform.up * handmovement, ForceMode.Impulse);
        }

        if (_bleftarm)
        {
            Leftarrm.AddForce(transform.up * handmovement, ForceMode.Impulse);
        }
    }

    private void Movearm(Rigidbody arm)
    {
        if (arm != null)
        {
            switches = handmovement;
            handmovement = temphandforce;
            temphandforce = switches;

            switches = upforce;
            upforce = tempupforce;
            tempupforce = switches;
        }
    }

    private void UpdateDownforce()
    {
        if (_brightarm || _bleftarm)
        {
            downforce = baseDownforce + upforce; 
        }
        else
        {
            downforce = baseDownforce; 
        }
    }

    public void distributedownforce()
    {
        // TODO: Implement force distribution for feet when walking
    }
}
