using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private GameObject textPopUp;
   

    private void Start()
    {
        textPopUp.SetActive(false);
        //detta måste göras så att den inte konstant är igång i början
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopUp.SetActive(true);
        }
      
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            { textPopUp.SetActive(false); }
    }
}
