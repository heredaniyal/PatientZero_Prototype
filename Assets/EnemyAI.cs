using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent monsterAgent;
    public Transform playerToChase;

    void Update()
    {
        // 1. Chase logic
        if (playerToChase != null)
        {
            monsterAgent.SetDestination(playerToChase.position);

            // 2. Distance Logic (No Physics needed)
            float distance = Vector3.Distance(transform.position, playerToChase.position);

            // Debugging: Print distance to console so we know it's working
            // Debug.Log("Distance: " + distance); 

            // If closer than 2.5 meters -> KILL
            if (distance < 2.5f)
            {
                Debug.Log("CAUGHT YOU! Restarting..."); // This will show in console
                KillPlayer();
            }
        }
    }

    void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}