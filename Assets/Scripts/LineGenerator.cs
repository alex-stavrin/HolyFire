using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum DrawMode
{
    None,
    Draw,
    Erase
}

public class LineGenerator : MonoBehaviour
{
    [SerializeField] GameObject line;
    [SerializeField] Image eraseButton;
    [SerializeField] Image drawButton;
    [SerializeField] Image fade;
    LineHandler lastLineHandler;
    DrawMode currentDrawMode;
    [SerializeField] float fadeDuration;

    float fadeTimer;

    bool endFade = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        currentDrawMode = DrawMode.None;
    }

    void Update()
    {

        if(fadeTimer < fadeDuration && !endFade)
        {
            fadeTimer += Time.deltaTime;

            float alpha = Mathf.Lerp(1, 0, fadeTimer / fadeDuration);
            fade.color = new Color(0,0,0,alpha);

            if(fadeTimer > fadeDuration)
            {
                StartDraw();
            }
        }

        if (endFade)
        {
            fadeTimer += Time.deltaTime;

            float alpha = Mathf.Lerp(0, 1, fadeTimer / fadeDuration);
            fade.color = new Color(0, 0, 0, alpha);

            if (fadeTimer > fadeDuration)
            {
                SceneManager.LoadScene("Menu");
            }
        }

        if (currentDrawMode == DrawMode.Draw)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject spawned = Instantiate(line);
                lastLineHandler = spawned.GetComponent<LineHandler>();
                lastLineHandler.shouldDraw = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                lastLineHandler.shouldDraw = false;
            }
        }
        else if (currentDrawMode == DrawMode.Erase)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = -Camera.main.transform.position.z;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                Collider2D hit = Physics2D.OverlapCircle(worldPos, 0.1f);
                if(hit)
                {
                    if(hit.gameObject)
                    {
                        Destroy(hit.gameObject);
                    }
                }
            }
        }
    }

    public void StartErase()
    {
        currentDrawMode = DrawMode.Erase;
        lastLineHandler.shouldDraw = false;
        eraseButton.color = Color.yellow;
        drawButton.color = Color.white;
    }

    public void StartDraw()
    {
        currentDrawMode= DrawMode.Draw;
        eraseButton.color = Color.white;
        drawButton.color = Color.yellow;
    }

    void OnDrawGizmos()
    {
        // only draw when the game is running
        if (!Application.isPlaying) return;

        // convert screen mouse to world
        Vector3 m = Input.mousePosition;
        m.z = -Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(m);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(worldPos, 0.1f);
    }

    public void EndGame()
    {
        lastLineHandler.shouldDraw=false;
        currentDrawMode = DrawMode.None;
        endFade = true;
        fadeTimer = 0;
    }
}
