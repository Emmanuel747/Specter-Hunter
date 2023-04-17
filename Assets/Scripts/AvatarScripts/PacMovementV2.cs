using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PacMovementV2 : MonoBehaviour
{
  public float moveForce = 0.8f; // The force applied to the object
  public float moveSpeed = 7f; // The speed at which Pacman moves
  public float maxSpeed = 8.9f; // The maximum speed Pacman can reach
  public float rotateSpeed = 1.5f; // The speed at which Pacman rotates
  public float jumpForce = 100f; // The force applied to the jump
  public float fallMultiplier = 50f; // The multiplier applied to the fall speed
  public float strafeForce = 10f; // The force applied for strafing

  public UnityEngine.AI.NavMeshAgent enemy;
  public Transform Player;

  private Vector3 initialMousePosition;
  // private bool mouseRightClickHeld = false;
  
  private Rigidbody rb; // The Rigidbody component of Pacman
  private int jumpCount = 1;
  public int consumedPellets = 0;
  public float rotSpeed = 1 ;

  private Score scoreScript; // Outside script variable

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    scoreScript = GameObject.Find("ScoreContainerCanvas").GetComponent<Score>();
  }

void Update()
{
    // Limit the maximum speed
    if (rb.velocity.magnitude > maxSpeed)
    {
        rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    // Move forwards and backwards based on current rotation
    Vector3 moveDirection = Vector3.zero;
    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
    {
        moveDirection -= transform.right;
    }
    else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
    {
        moveDirection += transform.right;
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

    // Apply movement force only if W, A, S, D are pressed
    if (moveDirection != Vector3.zero)
    {
        rb.AddForce(moveDirection * moveSpeed * moveForce, ForceMode.VelocityChange);
    } else if (jumpCount == 0) {
      rb.velocity = Vector3.zero;
    }

    // Increase fall speed
    if (rb.velocity.y < 0)
    {
        rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    // Double jump on press of spacebar
    if (Input.GetKeyDown(KeyCode.Space))
    {
        if (jumpCount < 2)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    // Strafe left and right
    if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
    {
        rb.AddForce(-transform.forward * strafeForce * moveForce, ForceMode.VelocityChange);
    }
    else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightArrow))
    {
        rb.AddForce(transform.forward * strafeForce * moveForce, ForceMode.VelocityChange);
    }

    // sending tracking data to enemy
    enemy.SetDestination(Player.position);
}

  // Reset jump count when Pacman lands on the ground
  void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "Ground")
      {
        jumpCount = 0;
      }

      // HardMode Game Mechanic where hitting walls = -10 score
      if (collision.gameObject.tag == "wall")
      {
        scoreScript.playerScoreDEC(10);
      }
    }

  // Consume Pellets upon collision & Update Completion %
  private void OnTriggerEnter(Collider Other) {
    if (Other.gameObject.CompareTag("Pellet")) {
      scoreScript.playerScoreINC(105);
      Destroy(Other.gameObject);
      consumedPellets += 1;
    } else if (Other.gameObject.CompareTag("PowerOrb")) {
      scoreScript.playerScoreINC(277);
      Destroy(Other.gameObject);
    }
  }
}