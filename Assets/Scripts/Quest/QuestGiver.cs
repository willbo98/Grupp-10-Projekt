using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private GameObject textPopUp;
   

    private void Start()
    {
        textPopUp.SetActive(false);
        //detta m�ste g�ras s� att den inte konstant �r ig�ng i b�rjan
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
