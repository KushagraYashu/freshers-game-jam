using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public CharacterController controller;
    public float gravity = -9.81f; // Earth's gravity
    public float speed = 12f;

    private Vector3 velocity; // Tracks vertical velocity
    private bool isGrounded; // Check if the player is on the ground

    public Transform groundCheck; // Empty GameObject at player's feet
    public float groundDistance = 0.4f; // Radius for ground check sphere
    public LayerMask groundMask; // Layer mask for the ground

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep grounded
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Move the player based on vertical velocity
        controller.Move(velocity * Time.deltaTime);
    }
}
