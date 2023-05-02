using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PacMovementV2 : MonoBehaviour
{
  public float moveForce = 0.8f;
  public float moveSpeed = 7f;
  public float maxSpeed = 8.9f;
  public float rotateSpeed = 1.5f;
  public float jumpForce = 100f;
  public float fallMultiplier = 50f;
  public float strafeForce = 10f;
  public float bounceMagnitude = 0.5f;
  public float bounceFrequency = 3f;

  public AudioSource proximitySpeaker;
  public AudioClip pelletCollectedSound;
  

  public UnityEngine.AI.NavMeshAgent enemy;
  public Transform Player;
  public Animator animator;

  private Vector3 initialMousePosition;
  private Rigidbody rb;
  private int jumpCount = 1;
  public int consumedPellets = 0;
  public float rotSpeed = 1;

  private Score scoreScript;
  private bool isMoving = false;
  private float bounceOffset;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    scoreScript = GameObject.Find("ScoreContainerCanvas").GetComponent<Score>();
    proximitySpeaker = gameObject.AddComponent<AudioSource>();
    proximitySpeaker.clip = pelletCollectedSound;
    proximitySpeaker.volume = 0.65f;
    proximitySpeaker.pitch = 0.5f;
  }

  void Update()
  {
    isMoving = false;
    animator.SetBool("isMoving", false);

    if (rb.velocity.magnitude > maxSpeed)
    {
      rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    Vector3 moveDirection = Vector3.zero;
    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
    {
      moveDirection -= transform.right;
      isMoving = true;
      animator.SetBool("isMoving", true);
    }
    else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
    {
      moveDirection += transform.right;
      isMoving = true;
      animator.SetBool("isMoving", true);
    }

    if (Input.GetKey(KeyCode.A))
    {
      transform.Rotate(0f, -rotateSpeed, 0f);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      transform.Rotate(0f, rotateSpeed, 0f);
    }

    if (moveDirection != Vector3.zero)
    {
      rb.AddForce(moveDirection * moveSpeed * moveForce, ForceMode.VelocityChange);
    }
    else if (jumpCount == 0)
    {
      rb.velocity = Vector3.zero;
    }

    if (rb.velocity.y < 0)
    {
      rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      if (jumpCount < 2)
      {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        jumpCount++;
      }
    }

    if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
    {
      rb.AddForce(-transform.forward * strafeForce * moveForce, ForceMode.VelocityChange);
    }
    else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightArrow))
    {
      rb.AddForce(transform.forward * strafeForce * moveForce, ForceMode.VelocityChange);
    }

    enemy.SetDestination(Player.position);

    // Walking bounce effect
    if (isMoving && jumpCount == 0)
    {
      bounceOffset = Mathf.Sin(Time.time * bounceFrequency) * bounceMagnitude;
      transform.position = new Vector3(transform.position.x, transform.position.y + bounceOffset, transform.position.z);
    }
  }

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
  private void OnTriggerEnter(Collider Other)
  {
    if (Other.gameObject.CompareTag("Pellet"))
    {
      scoreScript.playerScoreINC(105);
      proximitySpeaker.Play();
      Destroy(Other.gameObject);
      consumedPellets += 1;
    }
    else if (Other.gameObject.CompareTag("PowerOrb"))
    {
      scoreScript.playerScoreINC(277);
      proximitySpeaker.Play();
      Destroy(Other.gameObject);
    }
  }
}

