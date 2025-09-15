using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovementscript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text pineAppleText;
    [SerializeField] private GameObject pineAppleParticles, dustParticles;
    [SerializeField] private AudioClip jumpSound, pickupSound, healthUpSound;
    

    private float horizontalValue;
    private bool isGrounded;
    private bool canMove = true;
    private int startingHealth = 5;
    private int currentHealth = 0;
    public int pineAppleCollected = 0;
    //testar med vad chatgpt ville att man ska göra för dustparticles on landing men försöker förstå hur den menar
    private bool wasGrounded;
    //skickar ut en fråga via bool som har true or false


    private Rigidbody2D Rgbd;
    private SpriteRenderer rend;
    private Animator anim;
    private AudioSource audioSource;
    private float rayDistance = 0.25f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = startingHealth;
        pineAppleText.text = "" + pineAppleCollected;

        Rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if(horizontalValue < 0 )
        {
            FlipSprite(true);
        }
        if (horizontalValue > 0)
        {
            FlipSprite(false);
        }

        if (Input.GetButtonDown("Jump") && CheckIfGrounded() == true)
        {
            Jump();
        }
        anim.SetFloat("MoveSpeed", Mathf.Abs(Rgbd.linearVelocity.x));
        anim.SetFloat("VerticalSpeed", Rgbd.linearVelocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());
       
        
    }
    private void CanMoveAgain()
    {
        canMove = true;
       
    }

    private void FixedUpdate()
    {
        if(!canMove)
        {
            return;
        }
        Rgbd.linearVelocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, Rgbd.linearVelocity.y);

        bool isCurrentlyGrounded = CheckIfGrounded();

        if (!wasGrounded && isCurrentlyGrounded && Rgbd.linearVelocity.y <= 0f)
            //om jag inte var grounded och jag blir grounded och min velocity på Y axeln är mindre = eller mindre än 0f så sätt igång dustparticles.
        {
            Vector3 dustPos = transform.position + Vector3.down * 0.5f;
            //Jag fattar inte riktigt denna kod här. men 
            Instantiate(dustParticles, transform.position, Quaternion.identity);
        }
        wasGrounded = isCurrentlyGrounded;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PineApple"))
        {
            Destroy(other.gameObject);
            pineAppleCollected++;
            pineAppleText.text = "" + pineAppleCollected;
            audioSource.PlayOneShot(pickupSound, 0.2f);
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            Instantiate(pineAppleParticles, other.transform.position, Quaternion.identity);
        }
        if(other.CompareTag("HPUp"))
        {
            RestoreHealth(other.gameObject);
        }
    }
    private void FlipSprite(bool direction)
    {
        rend.flipX = direction;
    }
    private void Jump()
    {
        Rgbd.AddForce(new Vector2(0, jumpForce));
        Instantiate(dustParticles, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(jumpSound, 0.3f);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
    }
    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);


        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthbar();

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    public void TakeKnockBack(float knockbackForce, float upwards)
    {
        canMove = false;
        Rgbd.AddForce(new Vector2(knockbackForce, upwards));
        Invoke("CanMoveAgain", 0.25f);
    }
   
    private void Respawn()
    {

        currentHealth = startingHealth;
        UpdateHealthbar();
        transform.position = spawnPosition.position;
        Rgbd.linearVelocity = Vector2.zero;
    }
    private void UpdateHealthbar()
    {
        healthSlider.value = currentHealth;
        
    }
    private void RestoreHealth(GameObject healthPickup)
    {
        if (currentHealth >= startingHealth)
        {
            return;
        }
        else
        {
            int healthToRestore = healthPickup.GetComponent<HealthPickUp>().healthAmount;
            currentHealth += healthToRestore;
            UpdateHealthbar();
            audioSource.PlayOneShot(healthUpSound, 0.3f);
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            Destroy(healthPickup);

            if (currentHealth >= startingHealth)
            {
                currentHealth = startingHealth;
            }
        }
        
        
    }
}
