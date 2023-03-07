using UnityEngine;

public class RotateAndFloat : MonoBehaviour
{
    public float rotationSpeed = 24.85f;
    public float floatSpeed = 1.3f;
    public float floatAmplitude = 0.002f;

    private Vector3[] initialPositions;

    private void Start()
    {
        // Get the initial positions of the child objects
        int childCount = transform.childCount;
        initialPositions = new Vector3[childCount];
        for (int i = 0; i < childCount; i++)
        {
            initialPositions[i] = transform.GetChild(i).localPosition;
        }
    }

    private void Update()
    {
        foreach (Transform child in transform)
        {
            // Rotate child object on y-axis
            child.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // Add floating effect
            int childIndex = child.GetSiblingIndex();
            Vector3 childPos = initialPositions[childIndex];
            childPos.y += Mathf.Sin(Time.time * floatSpeed + child.GetInstanceID()) * floatAmplitude;
            child.localPosition = childPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pacman"))
        {
            Destroy(gameObject);
        }
    }
}
