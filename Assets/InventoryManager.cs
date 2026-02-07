using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory")]
    public bool hasKey = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CollectKey()
    {
        hasKey = true;
        Debug.Log("✅ Key Collected! Find the exit door.");
    }
}