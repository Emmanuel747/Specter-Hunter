using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip walkingSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = walkingSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current movement status of Pacman
        // bool isMoving = PacMovementV2.IsMoving();

        // Pause the audio if Pacman is not moving, and resume it if Pacman starts moving
        audioSource.pitch = true ? 1 : 0;
    }
}