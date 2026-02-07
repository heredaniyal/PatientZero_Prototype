using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public void OnInteract()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.CollectKey();
            Destroy(gameObject);
        }
    }
}