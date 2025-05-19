using UnityEngine;

public class HookShadowFollower : MonoBehaviour
{
    public Transform hook;
    public float fixedY = -0.1f;

    void Update()
    {
        if (hook != null)
        {
            Vector3 followPos = hook.position;
            followPos.y = fixedY; // keep it flat
            transform.position = followPos;
        }
    }
}