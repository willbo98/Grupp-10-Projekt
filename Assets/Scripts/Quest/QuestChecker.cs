using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox, finishedText, unfinishedText;
    [SerializeField] private int questGoal = 15;
    [SerializeField] private int levelToLoad;
    //
    [SerializeField] private BossHealth bossHealth;

    [SerializeField] Animator anim;
    [SerializeField] private AudioClip questSuccess, questFail;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        dialogueBox.SetActive(false);
        finishedText.SetActive(false);
        unfinishedText.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var PlayerPickup = other.GetComponent<PlayerPickup>();

            bool hasEnoughPineapples = PlayerPickup.pineAppleCollected >= questGoal;
            bool bossDefeated = bossHealth == null || bossHealth.isDead;
            if(other.GetComponent<PlayerPickup>().pineAppleCollected >= questGoal)
            {
                audioSource.PlayOneShot(questSuccess);
                dialogueBox.SetActive(true);
                finishedText.SetActive(true);
                anim.SetTrigger("Flag");
                Invoke("LoadNextlevel", 3.5f);
            }
            else
            {
                audioSource.PlayOneShot(questFail, 5f); 
                dialogueBox.SetActive(true);
                unfinishedText.SetActive(true); 
                
                
            }
        }

    }
    private void LoadNextlevel ()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            dialogueBox.SetActive(false);
            finishedText.SetActive(false);
            unfinishedText.SetActive(false);
        }
    }
}
