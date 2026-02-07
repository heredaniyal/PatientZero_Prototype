using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLogic : MonoBehaviour
{
    // [E] Is trigger par sirf tab kaam ho jab door open ho
    void OnTriggerEnter(Collider other)
    {
        // 1. Tag check (Best Practice)
        if (other.CompareTag("Player"))
        {
            Debug.Log("🏆 YOU ESCAPED! WINNER!");
            
            // 2. Win screen par bhejo (Build Settings mein scene add hona chahiye)
            // Agar Scene_Win nahi hai toh filhaal Restart chalne do:
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }
}