using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager Instance;

    [Header("Inventory Stats")]
    public int currentAmmo = 0;
    public bool hasKey = false;
    public int maxAmmo = 30;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo;
        Debug.Log("Ammo Added. Current: " + currentAmmo);
    }

    public void CollectKey()
    {
        hasKey = true;
        Debug.Log("Key Collected! Find the exit.");
    }
}