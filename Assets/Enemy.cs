using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        UnityEngine.Debug.Log("Enemy took damage! Remaining: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play death animation or particles here
        Destroy(gameObject);
    }
}
