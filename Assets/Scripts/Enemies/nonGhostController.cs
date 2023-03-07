using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class nonGhostController : MonoBehaviour {
    //Passed in GameObjects
    // public GameObject pacMan;
    // public Animator animator;
    // public NavMeshAgent navAgent;

    // // public options for devs to set during testing
    // public float moveSpeed = 14f;
    // public float runSpeedMultiplier = 5f; // add run speed multiplier
    // public float aniScaleSizeFloat = 16;

    // //Dev Bool to stop enemies from ending the game
    // public bool gameOverOn = false;

    // private void Start() {
    //     animator = GetComponent<Animator>();
    //     navAgent = GetComponent<NavMeshAgent>();
    //     navAgent.updateRotation = false;
    //     navAgent.enabled = true;
    // }

    private void Update() {
    //     if (pacMan != null) {
    //         // move towards pac-man
    //         float speed = moveSpeed;
    //         if (Vector3.Distance(transform.position, pacMan.transform.position) >= 5f) { // check distance to target
    //             speed *= 1.3f; // apply run speed multiplier   
    //             animator.SetBool("running", true);
    //         } else {
    //             animator.SetBool("running", true);
    //         }
    //         navAgent.speed = speed;
    //         navAgent.SetDestination(pacMan.transform.position);

    //         // determine which way to face based on movement direction
    //         if (transform.position.x < pacMan.transform.position.x) {
    //             transform.localScale = new Vector3(aniScaleSizeFloat, aniScaleSizeFloat, aniScaleSizeFloat); // facing right
    //         } else {
    //             transform.localScale = new Vector3(-aniScaleSizeFloat, aniScaleSizeFloat, aniScaleSizeFloat); // facing left
    //         }

    //         // set the speed of the run animation
    //         animator.SetFloat("Blend", moveSpeed * runSpeedMultiplier * 0.009f);
    //     }
    // }

    // private void OnCollisionEnter(Collision collision) {
    //   if (collision.gameObject.CompareTag("Pacman") && gameOverOn) {
    //         SceneManager.LoadScene("GameOver");
    //     } else if (collision.gameObject.CompareTag("wall")) {
    //         navAgent.ResetPath();
    //         Vector3 direction = pacMan.transform.position - transform.position;
    //         direction.y = 0;
    //         transform.rotation = Quaternion.LookRotation(direction);
    //     }
    }
}
