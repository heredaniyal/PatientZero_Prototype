using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLogic : MonoBehaviour
{
    // This runs when something walks INTO the green box (ESCAPING)
    void OnTriggerEnter(Collider other) // other is just a variable name
    {
        // Check if it is the Player
        if (other.gameObject.name == "PlayerCapsule")
        {
            Debug.Log("YOU ESCAPED! WINNER!"); // output statement
            
            // For now, we just restart the game (or load a Main Menu later)
            // Ideally, you would load a "Scene_Win" here.
            // Need to Talk with the Team here.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}