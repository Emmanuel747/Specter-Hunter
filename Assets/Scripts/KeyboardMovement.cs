using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    public float moveSpeed = 30f; // The speed at which Pacman moves
    public float rotateSpeed = 15f; // The speed at which the camera rotates
    private Vector3 moveDirection; // The direction in which Pacman moves
    private float rotateAmount; // The amount to rotate Pacman on the y-axis
    private Rigidbody rb; // The Rigidbody component of Pacman

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input for movement
        moveDirection = new Vector3(-Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDirection *= moveSpeed;

        // Apply movement
        rb.AddForce(moveDirection, ForceMode.Force);

    //         // Get input for movement
    // moveDirection = new Vector3(-Input.GetAxis("Horizontal"), 0f, -Input.GetAxis("Vertical"));
    // moveDirection *= moveSpeed;
  }
}