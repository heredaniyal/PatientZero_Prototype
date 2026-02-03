using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepSource;
    public CharacterController controller;

    void Update()
    {
        // Check if player is moving on the ground
        // velocity.magnitude > 0.1 means we are moving
        if (controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            // If sound is NOT playing, start it
            if (!footstepSource.isPlaying)
            {
                footstepSource.Play();
            }
        }
        else
        {
            // If we stopped moving, stop the sound
            if (footstepSource.isPlaying)
            {
                footstepSource.Stop();
            }
        }
    }
}