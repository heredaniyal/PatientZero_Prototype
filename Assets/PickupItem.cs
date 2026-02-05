using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public enum ItemType { Ammo, Key }

    [Header("Item Settings")]
    public ItemType type;
    public int ammoAmount = 10;

    public void OnInteract()
    {
        if (type == ItemType.Ammo)
        {
            if (AmmoManager.Instance != null)
            {
                AmmoManager.Instance.AddAmmo(ammoAmount);
                Destroy(gameObject); // Remove item
            }
        }
        else if (type == ItemType.Key)
        {
            if (AmmoManager.Instance != null)
            {
                AmmoManager.Instance.CollectKey();
                Destroy(gameObject); // Remove key
            }
        }
    }
}