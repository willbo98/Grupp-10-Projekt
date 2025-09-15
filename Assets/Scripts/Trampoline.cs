using UnityEngine;
using UnityEngine.Audio;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private AudioClip superJumpSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
     if(other.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            playerRigidbody.linearVelocity = new Vector2 (playerRigidbody.linearVelocity.x, 0f);
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            GetComponent<Animator>().SetTrigger("Jump");
            audioSource.PlayOneShot(superJumpSound, 0.3f);
            audioSource.pitch = Random.Range(0.9f, 1.1f);
        }
    }
}
