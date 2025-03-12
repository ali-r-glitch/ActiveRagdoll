using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;  // Reference to player to follow
    [SerializeField] private Vector3 offset = new Vector3(0, 3, -5); // Position offset
    [SerializeField] private float smoothSpeed = 5f;  // Smooth follow speed
    [SerializeField] private float rotationSpeed = 50f; // Rotation speed when pressing Q/E

    private float currentAngle = 0f; // To store current horizontal rotation angle

    void LateUpdate()
    {
        HandleRotationInput();
        FollowPlayer();
    }

    // Rotate the camera around the player when pressing Q and E
    private void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentAngle -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            currentAngle += rotationSpeed * Time.deltaTime;
        }
    }

    // Smooth follow and position update
    private void FollowPlayer()
    {
        // Calculate rotated offset
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 desiredPosition = player.position + rotation * offset;

        // Smooth movement to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Always look at the player
        transform.LookAt(player);
    }
}