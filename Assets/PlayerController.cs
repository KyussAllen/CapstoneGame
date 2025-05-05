using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Tuning")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isFacingRight = true;



    [Header("Combat Settings")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;


    private Rigidbody2D rb;
    private bool isGrounded;


    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * (isFacingRight ? 1 : -1);
        transform.localScale = localScale;
    }




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        BetterJump();
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if (horizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && isFacingRight)
        {
            Flip();
        }
  

    }


    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    void BetterJump()
    {
        if (rb.linearVelocity.y < 0)
        {
            // Falling
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            // Jumping but player let go of jump button, aka short hops
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public float rotationSpeed = 10f; 

   /* 
      private void FixedUpdate()
    {
        // Keep feet pointing down
        Vector3 groundDirection = Vector3.down;
        Quaternion targetRotation = Quaternion.LookRotation(groundDirection, transform.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    */
    void Attack()
    {
        //Add attavk animation here Kyuss

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
        // safety incase attackPoint is not assigned
        if (attackPoint == null)
        {
            UnityEngine.Debug.LogWarning("AttackPoint not assigned!");
            return;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check ground collision
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
