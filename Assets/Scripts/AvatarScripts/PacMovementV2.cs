using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PacMovementV2 : MonoBehaviour
{
  public float moveForce = 1.5f; // The force applied to the object
  public float moveSpeed = 27f; // The speed at which Pacman moves
  public float maxSpeed = 35f; // The maximum speed Pacman can reach
  public float rotateSpeed = 0.5f; // The speed at which Pacman rotates
  public float jumpForce = 150f; // The force applied to the jump
  public float fallMultiplier = 2.5f; // The multiplier applied to the fall speed
  public float strafeForce = 20f; // The force applied for strafing

  public UnityEngine.AI.NavMeshAgent enemy;
  public Transform Player;

  private Vector3 initialMousePosition;
  // private bool mouseRightClickHeld = false;
  
  private Rigidbody rb; // The Rigidbody component of Pacman
  private int jumpCount = 0;
  public int consumedPellets = 0;
  public float rotSpeed = 5 ;

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
    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.RightArrow))
    {
        rb.AddForce((-transform.right * moveSpeed) * moveForce, ForceMode.Force);
    }
    else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
    {
        rb.AddForce((transform.right * moveSpeed) * moveForce, ForceMode.Force);
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
        rb.AddForce(-transform.forward* strafeForce * moveForce, ForceMode.Force);
    }
    else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightArrow))
    {
      rb.AddForce(transform.forward * strafeForce * moveForce, ForceMode.Force);
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




  // public bool IsMoving() {
  //   return rb.velocity != Vector3.zero;  
  // }
}
    // [Work in Progress] Rotate based on mouse right-click drag direction
    // if (Input.GetMouseButtonDown(1))
    // {
    //     mouseRightClickHeld = true;
    //     initialMousePosition = Input.mousePosition;
    //     Debug.Log(initialMousePosition);
    // }
    // else if (Input.GetMouseButtonUp(1))
    // {
    //     mouseRightClickHeld = false;
    // }

    // if (mouseRightClickHeld)
    // {
    //   Vector3 currentMousePosition = Input.mousePosition;
    //   float rotationSpeed = (currentMousePosition - initialMousePosition).magnitude * 0.01f;
    //   // rotateSpeed = rotationSpeed <
      
    //   Vector3 rotationDirection = (currentMousePosition.x < initialMousePosition.x) ? -transform.right : transform.right;
    //   // mouseRotation = rotationDirection.x * rotationSpeed
    //   Debug.Log(rotationDirection);
    //   // transform.Rotate(0f, rotationDirection.x * 0.3f, 0f, Space.Self);
    //   if (rotationDirection.x > 0) {
    //     transform.Rotate(0f, rotateSpeed, 0f);
    //   } else transform.Rotate(0f, -rotateSpeed, 0f);

    // }
