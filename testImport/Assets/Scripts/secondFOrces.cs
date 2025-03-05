using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondFOrces : MonoBehaviour
{
    public float totalupforce;
    private float totaldownforce;
    private float headupforce;
    private float handipforce;
    private float leftlegdownforceforce;
    private float rightlegdownforceforce;

    public Rigidbody rightarm;
    public Rigidbody leftarm;
    public Rigidbody rightfoot;
    public Rigidbody leftfoot;
    public Rigidbody spine;

    private bool _brightarm = false;
    private bool _bleftarm = false;

    void Start()
    {
        totaldownforce = totalupforce;
        leftlegdownforceforce = totaldownforce;
        rightlegdownforceforce = leftlegdownforceforce;
        headupforce = totalupforce;
    }

    void Update()
    {
        HandleInput();
        ApplyForces();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DebugState("Pressed A");
            if (_brightarm) _brightarm = false;
            _bleftarm = !_bleftarm;
            calculateRatio();
           
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DebugState("Pressed D");
            if (_bleftarm) _bleftarm = false;
            _brightarm = !_brightarm;
            calculateRatio();
           
        }
    }

    void ApplyForces()
    {
        if (_brightarm)
        {
            rightarm.AddForce(transform.up * handipforce, ForceMode.Acceleration);
        }

        if (_bleftarm)
        {
            leftarm.AddForce(transform.up * handipforce, ForceMode.Acceleration);
        }

        spine.AddForce(transform.up * headupforce, ForceMode.Acceleration);
        leftfoot.AddForce(-transform.up * leftlegdownforceforce, ForceMode.Acceleration);
        rightfoot.AddForce(-transform.up * rightlegdownforceforce, ForceMode.Acceleration);
    }

    private void calculateRatio()
    {
        if (_brightarm || _bleftarm)
        {
            handipforce = totalupforce * 0.2f;
            headupforce = totalupforce * 0.8f;
        }
        else
        {
            handipforce = 0;
            headupforce = totalupforce;
        }
    }

    private void DebugState(string action)
    {
        Debug.Log($"Action: {action} | _brightarm: {_brightarm} | _bleftarm: {_bleftarm} | handipforce: {handipforce} | headupforce: {headupforce}");
    }
}
