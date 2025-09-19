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

    //William WallJump
    [SerializeField] private bool enableWallJumping = true; //toggle i main menu
    [SerializeField] private Transform wallCheckLeft, wallCheckRight;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private LayerMask whatIsWall;

    private float horizontalValue;
    private bool wasGrounded;
    private bool isGrounded;
    private bool canMove = true;
    private bool canDoubleJump = false;
    private Rigidbody2D Rgbd;
    private SpriteRenderer rend;
    private Animator anim;
    private AudioSource audioSource;
    private float rayDistance = 0.25f;

    //WilliamWallJump
    private bool isTouchingWall;
    private bool isWallSliding;
    [SerializeField] private float wallSlideSpeed = 0.5f;
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private Vector2 wallJumpDirection = new Vector2(1, 1);
    [SerializeField] private float wallJumpDuration = 0.2f;
    private bool isWallJumping;  
    private int wallDirection;

    //wallJump
    private void WallJump()
    {
        isWallJumping = true;
        Rgbd.linearVelocity = Vector2.zero;

        Vector2 jumpDirection = new Vector2(wallDirection, 1).normalized;
        Rgbd.AddForce(jumpDirection * wallJumpForce, ForceMode2D.Impulse);

        FlipSprite(wallDirection < 0);

        if (doubleJumpEnabled)
        {
            canDoubleJump = true;
        }

        Invoke(nameof(EndWallJump), wallJumpDuration);

    }
    private void EndWallJump()
    {
        isWallJumping = false;
    }

    void Start()
    {
        Rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    //WallJump
    void Update() //updaterad för att få walljump att funka
    {
        horizontalValue = Input.GetAxis("Horizontal");
        isGrounded = CheckIfGrounded();

        if (horizontalValue < 0)
        {
            FlipSprite(true);
        }
        if (horizontalValue > 0)
        {
            FlipSprite(false);
        }


        anim.SetFloat("MoveSpeed", Mathf.Abs(Rgbd.linearVelocity.x));
        anim.SetFloat("VerticalSpeed", Rgbd.linearVelocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());
        if (Input.GetButtonDown("Jump"))
        {
            if (enableWallJumping && isWallSliding && !isGrounded)
            {
                WallJump();
            }
            else if (isGrounded)
            {
                Jump();
            }
            else if (doubleJumpEnabled && canDoubleJump)
            {
                Jump();
            }
        }
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

        //WallJump
        if (enableWallJumping)
        {
            bool wallOnRight = Physics2D.Raycast(wallCheckRight.position, Vector2.right, wallCheckDistance, whatIsWall);
            bool wallOnLeft = Physics2D.Raycast(wallCheckLeft.position, Vector2.left, wallCheckDistance, whatIsWall);

            isTouchingWall = wallOnRight || wallOnLeft;

            wallDirection = wallOnRight ? -1 : (wallOnLeft ? 1 : 0);

            if (isTouchingWall && !isGrounded && horizontalValue != 0)
            {
                isWallSliding = true;
            }
            else
            {
                isWallSliding = false;
            }

            if (isWallSliding)
            {
                Rgbd.linearVelocity = new Vector2(Rgbd.linearVelocity.x, Mathf.Clamp(Rgbd.linearVelocity.y, -wallSlideSpeed, float.MaxValue));
            }
        }
        else
        {
            // If wall jump is disabled, reset related variables
            isWallSliding = false;
            isTouchingWall = false;
            wallDirection = 0;
        }
    }

    private void FlipSprite(bool direction) => rend.flipX = direction;

    private void Jump()
    {
        Rgbd.linearVelocity = new Vector2(Rgbd.linearVelocity.x, 0f);

        // Apply jump force
        Rgbd.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        Instantiate(dustParticles, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(jumpSound, 0.3f);
        audioSource.pitch = Random.Range(0.9f, 1.1f);

        if (doubleJumpEnabled)
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
