using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent monsterAgent;
    public Transform playerToChase;
    public Animator zombieAnim;
    
    // --- AUDIO VARIABLES ---
    public AudioSource zombieAudioSource; // The speaker on the zombie
    public AudioClip screamSound;         // The specific scream file
    private bool hasScreamed = false;     // Lock variable

    void Update()
    {
        if (playerToChase != null)
        {
            float distance = Vector3.Distance(transform.position, playerToChase.position);

            // 1. CHASE
            monsterAgent.SetDestination(playerToChase.position);

            // 2. ANIMATION
            if (monsterAgent.velocity.magnitude > 0.1f)
            {
                zombieAnim.SetBool("isRunning", true);
            }
            else
            {
                zombieAnim.SetBool("isRunning", false);
            }

            // Inside Update Loop...

            // 3. ATTACK & DAMAGE (Updated)
            if (distance < 1.5f && !hasScreamed)
            {
                // Play Scream
                zombieAudioSource.PlayOneShot(screamSound);
                hasScreamed = true; 

                // Animation
                zombieAnim.SetTrigger("attack");
                monsterAgent.isStopped = true; 

                // --- NEW DAMAGE LOGIC ---
                // Find the player's health script and hurt him
                PlayerHealth pHealth = playerToChase.GetComponent<PlayerHealth>();
                if(pHealth != null)
                {
                    pHealth.TakeDamage(25); // Deals 25 Damage
                }

                // Reset zombie after attack so he can chase again
                Invoke("ResetAttack", 2.0f);
            }
        }
    }

    void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ResetAttack()
    {
        hasScreamed = false;
        monsterAgent.isStopped = false; // Start moving again
    }
}