using System.Diagnostics;
using UnityEngine;
using System.Collections;
public class EnemyContactDamage : MonoBehaviour
{
    public int damageAmount = 1;
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
    private PlayerHealth playerHealth;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f); // Let everything initialize
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth == null)
                UnityEngine.Debug.LogError("PlayerHealth not found!");
        }
        else
        {
            UnityEngine.Debug.LogError("Player GameObject not found!");
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
