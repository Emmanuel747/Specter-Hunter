using UnityEngine;

public class Warp : MonoBehaviour
{

  private Transform player;
  private Vector3 targetPos;
  private bool isTeleporting = false;

  public Transform wallA;
  public Transform wallB;

  void Start()
  {
    player = GameObject.FindWithTag("Pacman").GetComponent<Transform>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Pacman") && !isTeleporting)
    {
      isTeleporting = true;
      if (gameObject.CompareTag("WarpWallA"))
      {
        targetPos = wallB.position;
      }
      else if (gameObject.CompareTag("WarpWallB"))
      {
        targetPos = wallA.position;
      }
      player.position = targetPos;

      isTeleporting = false;
    }
  }
}