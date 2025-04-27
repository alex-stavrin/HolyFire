using UnityEngine;

public enum EggColor
{
    RED,
    GREEN,
    BLUE,
}

public class Egg : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Rigidbody rb;

    public EggColor color;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.position;
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = startingPosition;
    }
}
