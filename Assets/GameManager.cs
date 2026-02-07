using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("✅ SoundManager Instance Created");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MakeNoise(Vector3 position, float range)
    {
        Debug.Log($"🔊 MakeNoise called: pos={position}, range={range}m");
        
        Collider[] hitColliders = Physics.OverlapSphere(position, range);
        Debug.Log($"   Found {hitColliders.Length} colliders in sphere");

        int zombiesFound = 0;
        foreach (var col in hitColliders)
        {
            Debug.Log($"   - {col.name} (Tag: '{col.tag}')");
            
            if (col.CompareTag("Enemy"))
            {
                zombiesFound++;
                ZombieAI zombie = col.GetComponent<ZombieAI>();
                if (zombie != null)
                {
                    zombie.HearSound(position);
                    Debug.Log($"   ✅ Zombie notified: {col.name}");
                }
                else
                {
                    Debug.LogWarning($"   ⚠️ {col.name} has Enemy tag but NO ZombieAI!");
                }
            }
        }
        
        if (zombiesFound == 0)
        {
            Debug.LogWarning("⚠️ NO ZOMBIES WITH 'Enemy' TAG FOUND IN RANGE!");
        }
    }
}