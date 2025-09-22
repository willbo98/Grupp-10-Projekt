using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float bounciness = 100;
    [SerializeField] private float knockBackForce = 200f;
    [SerializeField] private float upwardForce = 100f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private AudioClip getHitSound, hitSound;




    private SpriteRenderer rend;
    private bool canMove = true;
    private bool isActivated = false;
    private Transform player;
    private AudioSource audioSource;
    private bool canPlaySound = false;

    //allt script var f�ljt enligt videon men fick hj�lp att fixa s� att ljudeffekterna faktiskt var ordentligt fixade. 

    private void Start()
    {

        rend = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;

        Invoke(nameof(EnableSound), 0.2f);
    }

    private void EnableSound() => canPlaySound = true;


    private void FixedUpdate()
    {
        if (!canMove || !isActivated) return;
        if (player !=null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);
            rend.flipX = moveSpeed > 0;
        }
          
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("EnemyBlock") || other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Player") && canPlaySound)
        {

            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(hitSound, 0.3f);



            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageGiven);


            if (other.transform.position.x > transform.position.x)
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(knockBackForce, upwardForce);
            else
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(-knockBackForce, upwardForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true;
            player = other.transform;
        }

        if (other.CompareTag("Player") && canPlaySound)
        {
            var rb = other.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(new Vector2(0, bounciness));

            GetComponent<Animator>().SetTrigger("Hit");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;


            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(getHitSound, 0.3f);

            canMove = false;
            Destroy(gameObject, 0.45f);
        }
    }
    public void ActivateEnemy(Transform target)
    {
        isActivated = true;
        player = target;
    }
}