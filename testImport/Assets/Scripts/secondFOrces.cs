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
    private float legupforce;

    Rigidbody Mousehand;

    public Rigidbody rightarm;
    public Rigidbody leftarm;
    public Rigidbody rightfoot;
    public Rigidbody leftfoot;
    public Rigidbody spine;
    public Rigidbody rightthigh;
    public Rigidbody leftthigh;

    private bool _brightarm = false;
    private bool _bleftarm = false;
    private bool _brightleg = false;
    private bool _bleftleg = false;

    [SerializeField] private float followForce = 50f;
    [SerializeField] private float maxDistance = 2f;
    private float temppaxdistance;

    void Start()
    {
        totaldownforce = totalupforce;
        leftlegdownforceforce = totaldownforce;
        rightlegdownforceforce = leftlegdownforceforce;
        headupforce = totalupforce;
        temppaxdistance = maxDistance;
    }

    void Update()
    {
        HandleInput();
        ApplyForces();
        MouseMOvement();
    }

    private void MouseMOvement()
    {
        if (_brightarm)
            Mousehand = rightarm;
        else if (_bleftarm)
            Mousehand = leftarm;
        else if (_brightleg)
            Mousehand = rightfoot;
        else if (_bleftleg)
            Mousehand = leftfoot;
        else
            Mousehand = null;

        if (Mousehand != null)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.z)));

            Vector3 direction = targetPos - Mousehand.position;

            // Add smooth following force
            Mousehand.AddForce(direction * followForce, ForceMode.Force);

            // Limit distance
            if (direction.magnitude > maxDistance)
            {
                direction = direction.normalized * maxDistance;
                Mousehand.position = Mousehand.position + direction;
            }
        }
    }

    void HandleInput()
    {
        // Left Arm
        if (Input.GetKeyDown(KeyCode.A))
        {
            ResetSelection();
            _bleftarm = !_bleftarm;
            calculateRatio();
            DebugState("Pressed A");

            maxDistance = temppaxdistance;
        }

        // Right Arm
        if (Input.GetKeyDown(KeyCode.D))
        {
            ResetSelection();
            _brightarm = !_brightarm;
            calculateRatio();
            DebugState("Pressed D");
            maxDistance =temppaxdistance;
        }

        // Right Leg
        if (Input.GetKeyDown(KeyCode.E))
        {
            ResetSelection();
            _brightleg = !_brightleg;
            calculateRatio();
            DebugState("Pressed e");
            maxDistance = temppaxdistance*1.5f;
        }

        // Left Leg
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetSelection();
            _bleftleg = !_bleftleg;
            calculateRatio();
            DebugState("Pressed q");
            maxDistance =temppaxdistance*1.5f;
        }
    }

    void ResetSelection()
    {
        _brightarm = false;
        _bleftarm = false;
        _brightleg = false;
        _bleftleg = false;
    }

    void ApplyForces()
    {
        // Arms
        if (_brightarm)
            rightarm.AddForce(transform.up * handipforce, ForceMode.Acceleration);

        if (_bleftarm)
            leftarm.AddForce(transform.up * handipforce, ForceMode.Acceleration);

        // Legs
        if (_brightleg)
        {
            rightthigh.AddForce(transform.up * legupforce, ForceMode.Acceleration); // Thigh up
            rightfoot.AddForce(-transform.up * rightlegdownforceforce, ForceMode.Acceleration); // Foot down
            leftfoot.AddForce(-transform.up * leftlegdownforceforce, ForceMode.Acceleration); 
        }

        if (_bleftleg)
        {
            leftthigh.AddForce(transform.up * legupforce, ForceMode.Acceleration); // Thigh up
            leftfoot.AddForce(-transform.up * leftlegdownforceforce, ForceMode.Acceleration); // Foot down
            rightfoot.AddForce(-transform.up * rightlegdownforceforce, ForceMode.Acceleration); // Foot down
        }

        // General body support
        spine.AddForce(transform.up * headupforce, ForceMode.Acceleration);

        // Balancing feet when no legs selected
        if (!_brightleg && !_bleftleg)
        {
            leftfoot.AddForce(-transform.up * totaldownforce, ForceMode.Acceleration);
            rightfoot.AddForce(-transform.up * totaldownforce, ForceMode.Acceleration);
        }
    }

    private void calculateRatio()
    {
        // Reset
        handipforce = 0;
        legupforce = 0;
        headupforce = totalupforce;
        leftlegdownforceforce = totaldownforce;
        rightlegdownforceforce = totaldownforce;

        // Arms active
        if (_brightarm || _bleftarm)
        {
            handipforce = totalupforce * 0.2f;
            headupforce = totalupforce * 0.8f;
        }

        // Right leg active
        if (_brightleg)
        {
            legupforce = totalupforce * 0.2f;
            rightlegdownforceforce = totaldownforce * 0.5f;
            leftlegdownforceforce = totaldownforce * 1.5f;
        }

        // Left leg active
        if (_bleftleg)
        {
            legupforce = totalupforce * 0.2f;
            leftlegdownforceforce = totaldownforce * 0.5f;
            rightlegdownforceforce = totaldownforce * 1.5f;
        }
    }

    private void DebugState(string action)
    {
        Debug.Log($"Action: {action} | _brightarm: {_brightarm} | _bleftarm: {_bleftarm} | _brightleg: {_brightleg} | _bleftleg: {_bleftleg} | handipforce: {handipforce} | legupforce: {legupforce} | headupforce: {headupforce}");
    }
}
