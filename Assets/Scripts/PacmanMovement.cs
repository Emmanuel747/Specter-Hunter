using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour {
  public float moveSpeed = 25f; // The speed at which Pacman moves
  public float rotateSpeed = 0.41f; // The speed at which Pacman rotates
  public float jumpForce = 100f; // The force applied to the jump
  private Rigidbody rb; // The Rigidbody component of Pacman

  void Start() {
      rb = GetComponent<Rigidbody>();
  }

  void Update() {
    // Move forwards and backwards based on current rotation
    if (Input.GetKey(KeyCode.W))
    {
        rb.AddForce(-transform.right * moveSpeed, ForceMode.Force);
    }
    else if (Input.GetKey(KeyCode.S))
    {
        rb.AddForce(transform.right * moveSpeed, ForceMode.Force);
    }

    // Rotate left and right on Y axis
    if (Input.GetKey(KeyCode.A))
    {
        transform.Rotate(0f, -rotateSpeed, 0f);
    }
    else if (Input.GetKey(KeyCode.D))
    {
        transform.Rotate(0f, rotateSpeed, 0f);
    }

    // Jump on press of spacebar
    if (Input.GetKeyDown(KeyCode.Space))
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
  }
}
