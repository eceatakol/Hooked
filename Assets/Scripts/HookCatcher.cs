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
        //if (!hookController.IsActive()) return; // Only catch when hook is active (dropping or returning)

        if (other.CompareTag("Fish"))
        {
            FishBehavior fb = other.GetComponent<FishBehavior>();
            if (fb != null && !fb.isCaught)
        
            {
                fb.isCaught = true; // Mark the fish as caught to avoid double catching
                Debug.Log("ScoreManager.Instance: " + (ScoreManager.Instance == null ? "NULL" : "FOUND"));

                ScoreManager.Instance?.AddScore(1); // Increase score
                Debug.Log("Fish caught!");

                // Optional: disable the fish collider to prevent further collisions
                Collider fishCollider = other.GetComponent<Collider>();
                if (fishCollider != null)
                {
                    fishCollider.enabled = false;
                }
                // Destroy the fish object
                Destroy(other.gameObject);
            }
        }
    }
}