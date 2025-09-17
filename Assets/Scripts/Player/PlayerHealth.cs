using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 5;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private AudioClip healthUpSound;

    private int currentHealth = 0;
    private Rigidbody2D Rgbd;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = startingHealth;
        Rgbd = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        UpdateHealthbar();
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

    private void Respawn()
    {

        transform.position = spawnPosition.position;
        Rgbd.linearVelocity = Vector2.zero;
        currentHealth = startingHealth;
        UpdateHealthbar();
    }

    private void UpdateHealthbar()
    {
        healthSlider.value = currentHealth;
    }

    public void RestoreHealth(GameObject healthPickup)
    {
        if (currentHealth >= startingHealth) return;

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
