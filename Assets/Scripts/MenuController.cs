using UnityEngine;
using UnityEngine.SceneManagement;
//fixa så att main menu faktiskt funkar

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }
    public void closeCredits()
    {
        creditsPanel.SetActive(false);
    }
}
