using UnityEngine;
using TMPro;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private TMP_Text pineAppleText;
    [SerializeField] private GameObject pineAppleParticles;
    [SerializeField] private AudioClip pickupSound;

    public int pineAppleCollected = 0;

    private AudioSource audioSource;

    void Start()
    {
        pineAppleText.text = "" + pineAppleCollected;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PineApple"))
        {
            Destroy(other.gameObject);
            pineAppleCollected++;
            pineAppleText.text = "" + pineAppleCollected;
            audioSource.PlayOneShot(pickupSound, 0.2f);
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            Instantiate(pineAppleParticles, other.transform.position, Quaternion.identity);
        }

        if (other.CompareTag("HPUp"))
        {
            GetComponent<PlayerHealth>().RestoreHealth(other.gameObject);
        }
    }
}
