using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PacMovementV2 : MonoBehaviour
{
  public float fallMultiplier = 2.5f; // The multiplier applied to the fall speed
  public float strafeForce = 20f; // The force applied for strafing

  private Vector3 initialMousePosition;
  // private bool mouseRightClickHeld = false;
  
  private Rigidbody rb; // The Rigidbody component of Pacman
  private int jumpCount = 0;
  public int consumedPellets = 0;
  public float rotSpeed = 5 ;

  private Score scoreScript; // Outside script variable

  public Camera playerCamera;
  public float walkSpeed = 6f;
  public float runSpeed = 12f;
  public float jumpPower = 7f;
  public float gravity = 10f;


  public float lookSpeed = 2f;
  public float lookXLimit = 45f;

  public float health = 100f;


  Vector3 moveDirection = Vector3.zero;
  float rotationX = 0;

  public bool canMove = true;

  CharacterController characterController;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    scoreScript = GameObject.Find("ScoreContainerCanvas").GetComponent<Score>();
    characterController = GetComponent<CharacterController>();
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update()
  {
        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion
  }

  // Reset jump count when Pacman lands on the ground
  void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "Ground")
      {
        jumpCount = 0;
      }

      // HardMode Game Mechanic where hitting walls = -10 score
      if (collision.gameObject.tag == "Wall")
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
