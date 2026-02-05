using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void MakeNoise(Vector3 position, float range)
    {
        // Debug: Scene view mein yellow sphere dikhayega range check karne ke liye
        // Filhal hum invisible physics check laga rahe hain
        Collider[] hitColliders = Physics.OverlapSphere(position, range);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy")) // "Enemy" tag zaroori hai!
            {
                // Zombie mil gaya, usay signal bhejo
                hitCollider.GetComponent<ZombieAI>().HearSound(position);
            }
        }
    }
}