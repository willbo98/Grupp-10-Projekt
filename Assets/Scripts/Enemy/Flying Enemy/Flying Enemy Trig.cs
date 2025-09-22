using UnityEngine;

public class FlyingEnemyTrig : MonoBehaviour
{
    public FlyingEnemy enemy; // assign in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.ActivateEnemy(other.transform);
        }
    }
}
