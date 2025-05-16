using UnityEngine;

public class HookController : MonoBehaviour
{
    public float dropSpeed = 7f; // How fast the hook moves down
    public float riseSpeed = 5f; // How fast the hook moves back up
    public float maxDropDistance = 20f; // How far below the boat the hook can go

    private Vector3 initialLocalPosition; // Starting position (relative to boat)
    private bool isDropping = false; // Whether the player is holding the drop key
    private bool isReturning = false; // Whether the hook is returning to the boat

    void Start()
    {
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isDropping = true;
        }
        else
        {
            isDropping = false;
        }

        MoveHook();
    }

    void MoveHook()
    {
        if (isDropping)
        {
            // Move hook down while Space is pressed
            if (Mathf.Abs(transform.localPosition.y - initialLocalPosition.y) < maxDropDistance)
            {
                transform.localPosition += Vector3.down * dropSpeed * Time.deltaTime;
            }
        }
        else
        {
            // Move hook back up when Space is not pressed
            if (transform.localPosition.y < initialLocalPosition.y)
            {
                transform.localPosition += Vector3.up * riseSpeed * Time.deltaTime;
                if (transform.localPosition.y > initialLocalPosition.y)
                {
                    transform.localPosition = initialLocalPosition; // Snap exactly to start
                }
            }
        }
    }
public bool IsActive()
    {
        return isDropping || isReturning;
    }
}