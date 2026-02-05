using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepSource;
    public CharacterController controller;
    
    // Timer taake har frame mein spam na ho
    private float nextSoundTime = 0f; 

    void Update()
    {
        if (controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            // --- SOUND PLAYING LOGIC (Existing) ---
            if (!footstepSource.isPlaying)
            {
                footstepSource.Play();
            }

            // --- NOISE GENERATION LOGIC (New) ---
            if (Time.time >= nextSoundTime)
            {
                float currentSpeed = controller.velocity.magnitude;
                float noiseRange = 0f;

                if (currentSpeed > 5.0f) // Sprinting (Speed is 6)
                {
                    noiseRange = 15f; // Door tak awaaz jayegi
                    nextSoundTime = Time.time + 0.3f; // Jaldi jaldi updates
                }
                else // Walking (Speed is 4)
                {
                    noiseRange = 5f; // Kareeb tak awaaz jayegi
                    nextSoundTime = Time.time + 0.5f; // Slow updates
                }

                // Manager ko batao
                SoundManager.Instance.MakeNoise(transform.position, noiseRange);
            }
        }
        else
        {
            if (footstepSource.isPlaying) footstepSource.Stop();
        }
    }
}