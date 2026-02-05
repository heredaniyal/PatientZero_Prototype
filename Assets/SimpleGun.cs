using UnityEngine;
using UnityEngine.InputSystem; // Naya system use karne ke liye zaroori hai

public class SimpleGun : MonoBehaviour
{
    public float range = 100f;
    public Camera fpsCamera; 

    void Update()
    {
        // New Input System check for Left Mouse Click
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 1. Awaaz (SoundManager ko signal bhejo)
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.MakeNoise(transform.position, 30f);
            Debug.Log("Gun Fired! Noise created.");
        }

        // 2. Goli (Raycast)
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log("Goli lagi: " + hit.transform.name); 

            if (hit.transform.CompareTag("Enemy"))
            {
                ZombieAI zombie = hit.transform.GetComponent<ZombieAI>();
                if (zombie != null)
                {
                    zombie.TakeDamage(); // Zombie bhaag jaye ga
                }
            }
        }
    }
}