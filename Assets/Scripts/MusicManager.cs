using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioClip[] songs;
    public AudioSource audioSource;
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
