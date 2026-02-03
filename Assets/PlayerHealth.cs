using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    
    [Header("UI Reference")]
    public Slider healthSlider;
    public Image damagePanel; // The Red Panel
    
    [Header("Sounds")]
    public AudioSource playerAudio;
    public AudioClip hurtSound;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        // 1. Reduce Health
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        // 2. Play Hurt Sound (Optional)
        if(playerAudio && hurtSound) playerAudio.PlayOneShot(hurtSound);

        // 3. Flash Red Screen
        StartCoroutine(FlashRed());

        // 4. Check Death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Makes the screen flash red quickly
    IEnumerator FlashRed()
    {
        damagePanel.color = new Color(1, 0, 0, 0.5f); // Half red
        yield return new WaitForSeconds(0.1f);
        damagePanel.color = new Color(1, 0, 0, 0);    // Invisible
    }

    void Die()
    {
        // Restart Game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}