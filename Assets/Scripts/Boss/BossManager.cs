using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject bossUI;
    [SerializeField] private Slider bossHealthSlider;

    private BossHealth bossHealth;

    void Start()
    {
        GameObject boss = GameObject.Find("RockBoss");

        bossHealth = boss.GetComponent<BossHealth>();

    }

    void Update()
    {
        bossHealthSlider.value = bossHealth.bossHealth;

        if (bossHealth.bossHealth < 1)
        {
            bossUI.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossUI.SetActive(false);
        }
    }
}
