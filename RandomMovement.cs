using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float minMoveSpeed = 600f; // Minimum speed for linear movement
    public float maxMoveSpeed = 1400f; // Maximum speed for linear movement
    public float minRotationSpeed = 500f; // Minimum rotational speed
    public float maxRotationSpeed = 1800f; // Maximum rotational speed

    private Rigidbody2D rb;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = .09f;
        MoveRandomly();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            rb.velocity = Vector2.zero; // Stop linear movement
            rb.angularVelocity = 0f; // Stop rotation
            enabled = false; // Disable this script
        }
    }

    void MoveRandomly()
    {
        float randomMoveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed) * RandomSign();
        float randomRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed) * RandomSign();

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        rb.velocity = randomDirection * randomMoveSpeed; // Set linear velocity
        rb.angularVelocity = randomRotationSpeed; // Set rotational velocity
    }

    int RandomSign()
    {
        return Random.value < 0.5f ? -1 : 1; // Randomly returns -1 or 1
    }
}
