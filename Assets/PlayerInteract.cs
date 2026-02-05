using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRange = 3f;
    public LayerMask interactLayer;

    void Update()
    {
        // 1. Check Input
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("🟢 E Key Pressed!"); // Agar ye print na ho, to Input masla hai
            ShootRay();
        }
    }

    void ShootRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Visual debug (Scene view mein laal line dekho)
        Debug.DrawRay(transform.position, transform.forward * interactRange, Color.red, 2f);

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            Debug.Log("⚠️ Ray hit: " + hit.collider.name + " (Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer) + ")");

            // Check if object is in the right layer
            if (((1 << hit.collider.gameObject.layer) & interactLayer) != 0)
            {
                PickupItem item = hit.collider.GetComponent<PickupItem>();
                if (item != null)
                {
                    Debug.Log("✅ FOUND ITEM! Trying to pickup...");
                    item.OnInteract();
                }
                else
                {
                    Debug.Log("❌ Object is on Interactable layer BUT missing PickupItem script!");
                }
            }
            else
            {
                Debug.Log("❌ Hit object is NOT on Interactable Layer. Check Inspector settings!");
            }
        }
        else
        {
            Debug.Log("❌ Ray hit NOTHING. Get closer or aim better.");
        }
    }
}