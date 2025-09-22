using Unity.VisualScripting;
using UnityEngine;

public class Bossmusic : MonoBehaviour
{

    public AudioClip[] songs;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = songs[0];
        audioSource.Play();
    }


    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.clip = songs[1];
            audioSource.Play();
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.clip = songs[0];
            audioSource.Play();
        }
    }
}
