using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;

    private SpriteRenderer rend;
    private bool canMove = true;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);
        rend.flipX = moveSpeed > 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock") || other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }
    }

    public void DisableMovement()
    {
        canMove = false;
    }
}
