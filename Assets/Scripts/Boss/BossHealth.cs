using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float bounciness = 100;
    [SerializeField] private AudioClip getHitSound;
    [SerializeField] private AudioClip bossDeathSound;

    private MusicManager musicManager;

    public float bossHealth;

    private AudioSource audioSource;
    
    private bool canPlaySound = false;
    private BossMovement bossMovement;

    private void Start()
    {

        GameObject MusicPlayer = GameObject.Find("MusicPlayer");
        musicManager = MusicPlayer.GetComponent<MusicManager>();
        bossMovement = GetComponent<BossMovement>();

        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;

        Invoke(nameof(EnableSound), 0.2f);
    }

    private void EnableSound() => canPlaySound = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canPlaySound)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(getHitSound, 0.3f);
            var rb = other.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(new Vector2(0, bounciness));
            bossHealth--;
            print(bossHealth);

            if (bossHealth < 1)
            {


                GetComponent<Animator>().SetTrigger("Hit");
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<PolygonCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0f;
                GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

                bossMovement.DisableMovement();
                audioSource.PlayOneShot(bossDeathSound);
          //      musicManager.audioSource.clip = musicManager.songs[0];
          //      musicManager.audioSource.PlayDelayed(bossDeathSound.length);
                Destroy(gameObject, bossDeathSound.length);
                
                
            }

            
        }
    }
}
