using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float moveSpeed = 0.2f;        // How fast the fish swims
    public float turnSpeed = 25f;       // How fast the fish turns when rotating
    public float changeDirectionTime = 3f;  // How often to randomly change direction

    private float timeSinceLastChange = 0f;

    void Update()
    {
        // Move the fish forward continuously
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Update the timer
        timeSinceLastChange += Time.deltaTime;

        // If enough time has passed, randomly rotate
        if (timeSinceLastChange >= changeDirectionTime)
        {
            // Rotate randomly between -90 and 90 degrees
            float randomYRotation = Random.Range(-90f, 90f);
            transform.Rotate(0, randomYRotation, 0);

            // Reset timer
            timeSinceLastChange = 0f;
        }
    }
}