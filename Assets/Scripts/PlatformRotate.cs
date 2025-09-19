using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // degrees per second

    void Update()
    {
        // Rotate around Z-axis
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
