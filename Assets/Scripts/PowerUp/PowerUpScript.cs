using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public bool canDoubleJump = false;
    public bool canWallJump = false;

    public void EnableDoubleJump()
    {
        canDoubleJump = true;
    }
    public void EnableWallJump()
        { 
        canWallJump = true; 
    }
}
