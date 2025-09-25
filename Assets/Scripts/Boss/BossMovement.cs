using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;

    //jump
    [SerializeField] private bool canJump = false;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpinterval = 3f;

    private SpriteRenderer rend;
    private bool canMove = true;
    //jump
    private Rigidbody2D rb;
    private float jumpTimer;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        //jump
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = jumpinterval;
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);
        rend.flipX = moveSpeed > 0;
        //Boss jump
        if (canJump)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0f)
            {
                Jump();
                jumpTimer = jumpinterval + Random.Range(-1f, 1f);

            }
        }
    }
    private void Jump()
    {
        // Only jump if grounded (optional, requires ground check)
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock") || other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }
    }

    public void DisableMovement()
    {
        canMove = false;
    }

}
