using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class nonGhostController : MonoBehaviour
{
  // Passed in GameObjects
  public GameObject pacMan;
  public Animator animator;
  public NavMeshAgent navAgent;
  public AudioSource proximitySpeaker;
  public AudioSource globalSpeaker;

  public AudioClip ArrivalHorn;
  public AudioClip footsteps;

  // public options for devs to set during testing
  public float moveSpeed = 14f;
  public float runSpeedMultiplier = 5f; // add run speed multiplier
  public float aniScaleSizeFloat = 16;

  // Dev Bool to stop enemies from ending the game
  public bool gameOverOn = false;
  private Score scoreScript;
  private bool hasPlayedArrivalHorn = false;
  private bool isMoving = false;

  private void Start()
  {
    scoreScript = GameObject.Find("ScoreContainerCanvas").GetComponent<Score>();
    animator = GetComponent<Animator>();
    navAgent = GetComponent<NavMeshAgent>();
    navAgent.updateRotation = false;
    navAgent.enabled = true;
    proximitySpeaker = gameObject.AddComponent<AudioSource>();
    globalSpeaker = gameObject.AddComponent<AudioSource>();

  }

  private void Update()
  {
  navAgent.SetDestination(pacMan.transform.position);
    if (scoreScript.GetCompletionPercentage() > 35f)
    {
      navAgent.isStopped = false;
      if (!hasPlayedArrivalHorn)
      {
        globalSpeaker.clip = ArrivalHorn;
        globalSpeaker.volume = 0.45f; // 50% volume
        globalSpeaker.PlayOneShot(ArrivalHorn);
        hasPlayedArrivalHorn = true;
      }
    }
    else
    {
      navAgent.isStopped = true;
      hasPlayedArrivalHorn = false;
    }

    // Check that player1 has loaded first
    if (pacMan != null)
    {
      // move towards pac-man
      float speed = moveSpeed;

      CheckIsMoving(); // Check if the enemy is moving or not
      // plays footstep sounds when moving
      if (isMoving)
      {
        if (!proximitySpeaker.isPlaying)
        { // play footsteps on loop
          proximitySpeaker.clip = footsteps;
          
        }
      }
      else { proximitySpeaker.Stop(); }

      if (isMoving) {
        // check distance to target
        if (Vector3.Distance(transform.position, pacMan.transform.position) >= 95f)
        {
          speed *= 1.5f; // apply run speed multiplier
          animator.SetBool("walking", false);   
          animator.SetBool("running", true);
        }
        else { 
          speed *= 1f; // apply run speed multiplier
          animator.SetBool("running", false);
          animator.SetBool("walking", true); 
        }
      }
      // updates navAgent path 
      navAgent.speed = speed;
      navAgent.SetDestination(pacMan.transform.position);

      // determine which way to face based on movement direction
      if (transform.position.x < pacMan.transform.position.x)
      {
        transform.localScale = new Vector3(aniScaleSizeFloat, aniScaleSizeFloat, aniScaleSizeFloat); // facing right
        transform.LookAt(pacMan.transform);
      }
      else
      {
        transform.localScale = new Vector3(-aniScaleSizeFloat, aniScaleSizeFloat, aniScaleSizeFloat); // facing left
        transform.LookAt(pacMan.transform);
      }

      // set the speed of the run animation
      animator.SetFloat("Blend", moveSpeed * runSpeedMultiplier * 0.009f);
    }
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Pacman") && gameOverOn)
    {
      SceneManager.LoadScene("GameOver");
    }
    else if (collision.gameObject.CompareTag("wall"))
    {
      navAgent.ResetPath();
      Vector3 direction = pacMan.transform.position - transform.position;
      direction.y = 0;
      transform.rotation = Quaternion.LookRotation(direction);
    }
  }

  private void CheckIsMoving()
  {
    if (navAgent.velocity.magnitude > 0f)
    {
      isMoving = true;
    }
    else
    {
      isMoving = false;
    }
  }
}
