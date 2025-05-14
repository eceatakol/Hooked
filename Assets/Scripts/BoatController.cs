using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of boat movement
    public float xBoundary = 10f; // Limit left/right movement
    public float zBoundary = 10f; // Limit forward/backward movement

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float verticalInput = Input.GetAxis("Vertical"); // W/S or Up/Down

        // Move the boat horizontally (X) and forward/backward (Z)
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // Clamp position so boat stays within area
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xBoundary, xBoundary);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -zBoundary, zBoundary);
        transform.position = clampedPosition;
    }
}