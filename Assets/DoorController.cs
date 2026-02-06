using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator anim;
    private bool isOpen = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TryOpen()
    {
        if (isOpen) return; // Already open

        // Check Inventory for key
        if (AmmoManager.Instance.hasKey)
        {
            Debug.Log("✅ Door Unlocked! Escaping...");
            isOpen = true;
            
            // Trigger the animation
            if(anim != null)
            {
                anim.SetTrigger("Open"); 
            }
        }
        else
        {
            Debug.Log("❌ Door Locked! You need the Key.");
        }
    }
}