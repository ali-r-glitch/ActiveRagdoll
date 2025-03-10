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
    Rigidbody Mousehand;
    public Rigidbody rightarm;
    public Rigidbody leftarm;
    public Rigidbody rightfoot;
    public Rigidbody leftfoot;
    public Rigidbody spine;
    
    private bool _brightarm = false;
    private bool _bleftarm = false;

    [SerializeField] private float followForce= 50f;
    [SerializeField] float maxDistance = 2f;
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
        MouseMOvement();
    }

    private void MouseMOvement()
    {
        if (_brightarm)
            Mousehand = rightarm;
        else if (_bleftarm)
            Mousehand = leftarm;
        else
            Mousehand = null;

        if (Mousehand != null)
        {
            // Get target position from mouse
            Vector3 mousePos = Input.mousePosition;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.z)));

            // Calculate direction vector
            Vector3 direction = targetPos - Mousehand.position;

            // Option 1: Smooth force toward target (physics friendly)
              // You can tweak this value to make it stronger/weaker
            Mousehand.AddForce(direction * followForce, ForceMode.Force);

            // Option 2 (Alternative): Set velocity directly for smoother movement (optional to try)
            // float moveSpeed = 10f; // Adjust speed
            // Mousehand.velocity = direction * moveSpeed;

            // Optional: Limit distance to avoid overstretching
            // Max distance hand can be from body/shoulder
            if (direction.magnitude > maxDistance)
            {
                direction = direction.normalized * maxDistance;
                Mousehand.position = Mousehand.position + direction;
            }
        }
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
