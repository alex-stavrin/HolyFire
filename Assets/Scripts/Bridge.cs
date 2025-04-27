using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] float raiseSpeed = 10f;
    Vector3 endPosition;
    bool risen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endPosition = transform.position;
        transform.position = start.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(risen)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, raiseSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, start.position, raiseSpeed * Time.deltaTime);
        }
    }

    public void Raise()
    {
        risen = true;
    }

    public void Lower()
    {
        risen = false;
    }
}
