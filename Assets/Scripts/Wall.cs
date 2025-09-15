using UnityEngine;

public class Wall : MonoBehaviour
{
    private Animator anim;
    private bool hasPlayedAnimation = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !hasPlayedAnimation)
        {
            hasPlayedAnimation = true;
            anim.SetTrigger("Move");
        }
    }
}
