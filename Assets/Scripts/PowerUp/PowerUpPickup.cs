using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    public enum AbilityType { DoubleJump, WallJump }
    public AbilityType abilityToUnlock;
    [SerializeField] private AudioClip powerUp;

    [SerializeField] private GameObject powerUpParticle;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement ability = other.GetComponent<PlayerMovement>();
            if (ability != null)
            {
                switch (abilityToUnlock)
                {
                    case AbilityType.DoubleJump:
                        ability.EnableDoubleJump();
                        Instantiate(powerUpParticle, other.transform.position, Quaternion.identity);
                        break;
                    case AbilityType.WallJump:
                        ability.EnableWallJump();
                        Instantiate(powerUpParticle, other.transform.position, Quaternion.identity);
                        break;
                }
            }
            audioSource.PlayOneShot(powerUp, 1f);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject,powerUp.length); // remove pickup after use
        }
    }
}
