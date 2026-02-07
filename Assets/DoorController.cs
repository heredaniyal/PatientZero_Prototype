using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator anim;
    private bool isOpen = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log("🚪 DoorController initialized on: " + gameObject.name);
    }

    public void TryOpen()
    {
        Debug.Log("🚪 TryOpen() called!");
        
        if (isOpen)
        {
            Debug.Log("🚪 Door already open");
            return;
        }

        if (InventoryManager.Instance == null)
        {
            Debug.LogError("❌ InventoryManager.Instance is NULL!");
            return;
        }

        if (InventoryManager.Instance.hasKey)
        {
            Debug.Log("✅ Door Unlocked! Opening...");
            isOpen = true;
            
            if (anim != null)
            {
                anim.SetTrigger("Open");
                Debug.Log("🎬 Animation triggered");
            }
            else
            {
                Debug.LogWarning("⚠️ No Animator on door!");
            }

            // DOOR NOISE
            if (SoundManager.Instance != null)
            {
                Debug.Log("🔊 Making door noise at: " + transform.position);
                SoundManager.Instance.MakeNoise(transform.position, 25f);
            }
            else
            {
                Debug.LogError("❌ SoundManager.Instance is NULL!");
            }
        }
        else
        {
            Debug.Log("❌ Door Locked! hasKey = false");
        }
    }
}