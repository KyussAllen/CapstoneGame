using UnityEngine;
using System.Collections;
using System.Diagnostics;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    public float damageCooldown = 1f;
    private float lastDamageTime = -999f;
    public HealthBar healthBar;

    private PlayerHealth playerHealth;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar.SetMaxHealth(maxHealth);
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            UnityEngine.Debug.LogError("Player GameObject with tag 'Player' not found!");
            return;
        }

        playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            UnityEngine.Debug.LogError("PlayerHealth component not found on Player GameObject!");
        }
    }


  

    public void TakeDamage(int amount)
    {

        if (Time.time - lastDamageTime < damageCooldown)
            return;

        lastDamageTime = Time.time;

        currentHealth -= amount;
        StartCoroutine(FlashRed());
        healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        UnityEngine.Debug.Log("Player Died!");
        // Add death behavior 
    }
}
