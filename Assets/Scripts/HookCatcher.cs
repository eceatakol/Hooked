using UnityEngine;

public class HookCatcher : MonoBehaviour
{
    private HookController hookController;

    void Start()
    {
        hookController = GetComponentInParent<HookController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            FishBehavior fb = other.GetComponent<FishBehavior>();
            if (fb != null && !fb.isCaught)
            {
                fb.isCaught = true;
                int value = fb.pointValue;

                ScoreManager.Instance?.AddScore(value);
                AudioManager.Instance?.PlayCatchSound();
                Debug.Log($"Fish caught! Worth {value} points.");

                Collider fishCollider = other.GetComponent<Collider>();
                if (fishCollider != null) fishCollider.enabled = false;

                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Jellyfish"))
        {
            HandleFixedScoreCatch(other, -4, "Jellyfish");
        }
        else if (other.CompareTag("Trash"))
        {
            HandleFixedScoreCatch(other, -2, "Trash");
        }
    }

    private void HandleFixedScoreCatch(Collider obj, int scoreChange, string label)
    {
        ScoreManager.Instance?.AddScore(scoreChange);
        AudioManager.Instance?.PlayCatchSound();
        Debug.Log($"{label} caught! Worth {scoreChange} points");

        Collider col = obj.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Destroy(obj.gameObject);
    }
}