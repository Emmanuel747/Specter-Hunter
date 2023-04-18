using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RotateAndFloat : MonoBehaviour
{
    public float rotationSpeed = 24.85f;
    public float floatSpeed = 1.3f;
    public float floatAmplitude = 0.0015f;

    // public AudioSource proximitySpeaker;
    // public AudioClip pelletCollectedSound;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
        // proximitySpeaker = gameObject.AddComponent<AudioSource>();
        // proximitySpeaker.clip = pelletCollectedSound;
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
        if (other.CompareTag("Pacman"))
        {
          // ATM it does nothing
        }
    }
}
