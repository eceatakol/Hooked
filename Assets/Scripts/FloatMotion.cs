using UnityEngine;

public class FloatMotion : MonoBehaviour
{
    public float amplitude = 0.2f;  // Height of the bobbing
    public float frequency = 1f;     // Speed of the bobbing

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}