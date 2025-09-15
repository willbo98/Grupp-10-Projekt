using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private Transform target1, target2;
    [SerializeField] private float moveSpeed = 2.0f;

    private Transform currentTarget;
    void Start()
    {
        currentTarget = target1;

    }


    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target1.position) < 0.01f)
        {
            currentTarget = target2;
        }
        if (Vector2.Distance(transform.position, target2.position) < 0.01f)
        {
            currentTarget = target1;
        }
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.transform.position.y > transform.position.y)
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
