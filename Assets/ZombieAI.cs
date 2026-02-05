using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public AudioSource zombieAudio;
    public AudioClip screamSound;
    private bool isRetreating = false;

    // --- YE METHOD HONA ZAROORI HAI ---
    public void TakeDamage()
    {
        if (isRetreating) return;

        Debug.Log("Zombie Hit! Retreating...");
        if(zombieAudio && screamSound) zombieAudio.PlayOneShot(screamSound);

        // Door bhaagne ka point
        Vector3 retreatPos = transform.position - (transform.forward * 20f); 
        
        isRetreating = true;
        agent.speed = 6f; 
        agent.SetDestination(retreatPos);
        anim.SetBool("isRunning", true);
        
        Invoke("ResetSpeed", 5f);
    }

    public void HearSound(Vector3 noisePos)
    {
        if (isRetreating) return;
        agent.SetDestination(noisePos);
        anim.SetBool("isRunning", true);
    }

    void ResetSpeed() { isRetreating = false; agent.speed = 3.5f; anim.SetBool("isRunning", false); }
}