using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 500f;
    public float xBoundary = 10f;
    public float zBoundary = 10f;

    private Vector3 movementInput;

    void Update()
    {
        HandleMovement();
        HandleRotation();
        ClampPosition();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movementInput = new Vector3(horizontalInput, 0, verticalInput);

        transform.Translate(movementInput * moveSpeed * Time.deltaTime, Space.World);
    }

    private void HandleRotation()
    {
        if (movementInput != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementInput, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void ClampPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xBoundary, xBoundary);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -zBoundary, zBoundary);
        transform.position = clampedPosition;
    }
}