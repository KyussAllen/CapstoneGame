using System.Diagnostics;
using UnityEngine;

public class Enemy :  MonoBehaviour
{
    public int health = 10;

    public void TakeDamage(int damage)
    {
        health -= damage;
        UnityEngine.Debug.Log("Enemy took damage! Remaining: " + health);

        if (health <= 0)
        {
            Die();
        }
    }


public class EnemyContactDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public Transform frontCheck;
    public float frontRange = 0.5f;
    public LayerMask playerLayer;

    private void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(frontCheck.position, frontRange, playerLayer);
        if (hit != null)
        {
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (frontCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(frontCheck.position, frontRange);
        }
    }
}

void Die()
    {
        // Play death animation or particles here
        Destroy(gameObject);
    }
}
