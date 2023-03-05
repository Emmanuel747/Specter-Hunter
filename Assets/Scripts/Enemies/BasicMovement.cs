using UnityEngine;

public class BasicMovement : MonoBehaviour
{
  public float moveSpeed;

  public float jumpForce;

  private bool canJump;
  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    playerMove();
  }

  public void playerMove()
  {

    Vector3 Move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    Move = Vector3.ClampMagnitude(Move, 1);

    transform.Translate(Move * Time.deltaTime * moveSpeed);


    if (Input.GetKeyDown(KeyCode.Space))
    {
      if (canJump == true)
      {
        Rigidbody2D playerRB = transform.GetComponent<Rigidbody2D>();
        playerRB.AddForce(Vector2.up * jumpForce);
      }

    }
  }
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Ground")
    {
      canJump = true;
    }
  }
  private void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Ground")
    {
      canJump = false;
    }
  }
}