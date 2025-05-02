using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndVolume : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] GameObject player;
    [SerializeField] float fadeDuration;
    [SerializeField] Image fade;

    float fadeTimer;

    bool fading = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.SetActive(false);
            fading = true;
            controller.enabled = false;
        }
    }

    private void Update()
    {
        if(fading)
        {
            fadeTimer += Time.deltaTime;

            float alpha = Mathf.Lerp(0, 1, fadeTimer / fadeDuration);
            fade.color = new Color(0, 0, 0, alpha);

            if (fadeTimer - 0.5f > fadeDuration)
            {
                SceneManager.LoadScene("Draw");
            }
        }
    }
}