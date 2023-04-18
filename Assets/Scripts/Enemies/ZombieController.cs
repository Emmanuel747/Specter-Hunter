using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieController : MonoBehaviour
{
  public GameObject pacMan;
  public float moveSpeed = 5f;
  public float runSpeedMultiplier = 1f; // add run speed multiplier
  public Animator animator;
  public float aniScaleSizeFloat = 16;
  public bool gameOverOn = false;

  private void Start()
  {
    animator = GetComponent<Animator>();
  }

  private void Update()
  {
    if (pacMan != null)
    {
      // move towards pac-man
      float speed = moveSpeed;
      if (Vector3.Distance(transform.position, pacMan.transform.position) <= 5f)
      { // check distance to target
        speed *= runSpeedMultiplier; // apply run speed multiplier   
        animator.SetBool("running", true);
      }
      else
      {
        animator.SetBool("running", true);
      }
      transform.position = Vector3.MoveTowards(transform.position, pacMan.transform.position, speed * Time.deltaTime);

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
      animator.SetFloat("Blend", moveSpeed * (runSpeedMultiplier * 0.009f));
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
      Vector3 direction = pacMan.transform.position - transform.position;
      direction.y = 0;
      transform.rotation = Quaternion.LookRotation(direction);
    }
  }
}


// animator.SetBool("walking", true);
// animator.SetBool("walkingBackwards", false);

// animator.SetBool("runningBackwards", true);
// animator.SetBool("turning", true);