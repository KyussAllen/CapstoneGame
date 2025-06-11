using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(1);
        }

        
        if (impactEffect != null)
        {
            GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
            Animator anim = effect.GetComponent<Animator>();

            if (anim != null)
            {
                float animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
                Destroy(effect, animDuration);
            }
            else
            {
                Destroy(effect, 1f); 
            }
        }

        Destroy(gameObject);
    }
}
