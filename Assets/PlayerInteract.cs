using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRange = 3f;
    public LayerMask interactLayer;

    void Update()
    {
        // 'E' Key check
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("🟢 E Key Pressed!");
            ExecuteInteraction();
        }
    }

    void ExecuteInteraction()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        
        // Raycast sirf Interactable layer ko hit karega
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
        {
            Debug.Log("⚠️ Ray hit: " + hit.collider.name);

            // 1. Check for Items (Ammo/Key)
            if (hit.collider.TryGetComponent(out PickupItem item))
            {
                item.OnInteract();
            }
            // 2. Check for Door (IMPORTANT: Checking Parent too!)
            else if (hit.collider.GetComponentInParent<DoorController>() != null)
            {
                hit.collider.GetComponentInParent<DoorController>().TryOpen();
            }
            else
            {
                Debug.Log("❌ Hit an Interactable but no valid script found!");
            }
        }
    }
}