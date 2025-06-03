using UnityEngine;
using System.Collections;
using System.Diagnostics;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    public float damageCooldown = 1f;
    private float lastDamageTime = -999f;
    private PlayerHealth playerHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
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


    public void TakeDamage(int damage)
    {

        if (Time.time - lastDamageTime < damageCooldown)
            return;

        lastDamageTime = Time.time;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        StartCoroutine(FlashRed());
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
      
    }
}
