using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Tuning")]
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 4f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    private bool isFacingRight = true;

    [Header("Advanced Jump Settings")]
    public float coyoteTime = 0.2f; // WileyCoyote type jump 
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.2f; // Jump input memory
    private float jumpBufferCounter;


    [Header("Combat Settings")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;


    private Rigidbody2D rb;


    void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }



    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

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
    void FixedUpdate()
    {
        CheckGrounded();
        Move();
        Jump();
        HandleJumpInput();
        BetterJump();
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
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
    void HandleJumpInput()
    {
        // Count down coyote time when in air
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferCounter = 0f; // Reset buffer after jump
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


    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

}