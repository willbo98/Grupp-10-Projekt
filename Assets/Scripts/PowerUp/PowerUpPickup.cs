using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    public enum AbilityType { DoubleJump, WallJump }
    public AbilityType abilityToUnlock;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement ability = other.GetComponent<PlayerMovement>();
            if (ability != null)
            {
                switch (abilityToUnlock)
                {
                    case AbilityType.DoubleJump:
                        ability.EnableDoubleJump();
                        break;
                    case AbilityType.WallJump:
                        ability.EnableWallJump();
                        break;
                }
            }

            Destroy(gameObject); // remove pickup after use
        }
    }
}
