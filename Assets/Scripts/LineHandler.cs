using UnityEngine;
 
public class LineHandler : MonoBehaviour
{
    Vector3 lastPoint;
    LineRenderer lineRenderer;
    EdgeCollider2D edgeCollider;
    [SerializeField] float detail = 0.5f;

    public bool shouldDraw = false;

    void Start()
    {
       edgeCollider = GetComponent<EdgeCollider2D>();
       lineRenderer = GetComponent<LineRenderer>();


       lastPoint = GetMouse();

       lineRenderer.SetPosition(0, GetMouse());

        UpdateCollisions();
    }

    void Update()
    {
        if(shouldDraw)
        {
            Vector3 currentMousePos = GetMouse();
            if(Vector3.Distance(lastPoint, currentMousePos) > detail)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentMousePos);
                lastPoint = currentMousePos;
                UpdateCollisions();
            }
        }
    }

    Vector3 GetMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void UpdateCollisions()
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);

        Vector2[] vec2Array = new Vector2[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            vec2Array[i] = (Vector2)positions[i];
        }

        edgeCollider.points = vec2Array;
    }
}
