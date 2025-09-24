using UnityEngine;

public class BossDamage : MonoBehaviour
{
    [SerializeField] private float knockBackForce = 200f;
    [SerializeField] private float upwardForce = 100f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private AudioClip hitSound;

    private AudioSource audioSource;
    private bool canPlaySound = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;

        Invoke(nameof(EnableSound), 0.2f);
    }

    private void EnableSound() => canPlaySound = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
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
}
