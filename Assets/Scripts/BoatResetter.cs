using UnityEngine;

public class BoatResetter : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Record the boat's original position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void ResetBoat()
    {
        // Reset to the original position and rotation
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}