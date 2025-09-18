using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private GameObject dustParticles;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private bool doubleJumpEnabled;

    private float horizontalValue;
    private bool wasGrounded;
    private bool canMove = true;
    private bool canDoubleJump = false;
    private Rigidbody2D Rgbd;
    private SpriteRenderer rend;
    private Animator anim;
    private AudioSource audioSource;
    private float rayDistance = 0.25f;

    void Start()
    {
        Rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if (horizontalValue < 0) FlipSprite(true);
        if (horizontalValue > 0) FlipSprite(false);

        if (Input.GetButtonDown("Jump") && (CheckIfGrounded() == true || canDoubleJump == true))
        {
            Jump();
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs(Rgbd.linearVelocity.x));
        anim.SetFloat("VerticalSpeed", Rgbd.linearVelocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        Rgbd.linearVelocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, Rgbd.linearVelocity.y);

        bool isCurrentlyGrounded = CheckIfGrounded();

        if (!wasGrounded && isCurrentlyGrounded && Rgbd.linearVelocity.y <= 0f)
        {
            Instantiate(dustParticles, transform.position, Quaternion.identity);
        }
        wasGrounded = isCurrentlyGrounded;
    }

    private void FlipSprite(bool direction) => rend.flipX = direction;

    private void Jump()
    {
        Rgbd.AddForce(new Vector2(0, jumpForce));
        Instantiate(dustParticles, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(jumpSound, 0.3f);
        audioSource.pitch = Random.Range(0.9f, 1.1f);

        if (doubleJumpEnabled == true)
        {
            canDoubleJump = false;
        }
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") ||
            rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            if (doubleJumpEnabled == true)
            {
                canDoubleJump = true;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeKnockBack(float knockbackForce, float upwards)
    {
        canMove = false;
        Rgbd.AddForce(new Vector2(knockbackForce, upwards));
        Invoke("CanMoveAgain", 0.25f);
    }

    private void CanMoveAgain() => canMove = true;
}
