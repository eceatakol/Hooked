using UnityEngine;

public class HookCatcher : MonoBehaviour
{
    private HookController hookController;

    void Start()
    {
        // Get the HookController from the parent object
        hookController = GetComponentInParent<HookController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Optional: Uncomment this if you only want to catch during active drop/return
        // if (!hookController.IsActive()) return;

        if (other.CompareTag("Fish"))
        {
            FishBehavior fb = other.GetComponent<FishBehavior>();
            if (fb != null && !fb.isCaught)
            {
                fb.isCaught = true; // Prevent double catch

                int value = fb.pointValue; // Get points from fish
                ScoreManager.Instance?.AddScore(value); // Add correct points
                AudioManager.Instance?.PlayCatchSound();
                Debug.Log($"Fish caught! Worth {value} points.");

                // Disable collider (optional safety)
                Collider fishCollider = other.GetComponent<Collider>();
                if (fishCollider != null)
                {
                    fishCollider.enabled = false;
                }

                Destroy(other.gameObject); // Remove fish
            }
        }
    }
}