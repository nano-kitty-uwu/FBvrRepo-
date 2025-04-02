using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private Vector3 target;
    private float speed;

    public void Initialize(Vector3 playerPosition, float asteroidSpeed)
    {
        target = playerPosition;
        speed = asteroidSpeed;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage();
            Destroy(gameObject); 
        }
    }
}
