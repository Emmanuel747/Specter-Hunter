using UnityEngine;

public class RotateAndFloat : MonoBehaviour
{
    public float rotationSpeed = 24.85f;
    public float floatSpeed = 1.3f;
    public float floatAmplitude = 0.0015f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        // Rotate pellet on y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Add floating effect
        Vector3 pelletPos = initialPosition;
        pelletPos.y += Mathf.Sin(Time.time * floatSpeed + transform.GetInstanceID()) * floatAmplitude;
        transform.localPosition = pelletPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pellet"))
        {
            Destroy(other.gameObject);
        }
    }
}
