using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float maxIntroSpeed = 50;
    [SerializeField] float introAcceleration = 2f;
    [SerializeField] GameObject introCamera;
    [SerializeField] GameObject playerCamera;
    [SerializeField] PlayerController playerController;

    [SerializeField] float timeToShowText;
    [SerializeField] Image fade;
    [SerializeField] float fadeDuration;
    [SerializeField] float timeToShowHouse;
    [SerializeField] TextMeshProUGUI goalText;
    [SerializeField] Animation textAnimation;

    float fadeTimer = 0;
    int currentPoint = 0;
    bool playingIntro = false;
    bool fading = false;
    bool fadeReverse = false;

    float currentIntroSpeed = 0;

    [SerializeField]  bool skipIntro = false;

    IEnumerator Intro()
    {
        textAnimation.Play();
        playerController.enabled = false;
        playerCamera.SetActive(false);
        introCamera.SetActive(true);
        yield return new WaitForSeconds(timeToShowText);
        fading = true;
        yield return new WaitForSeconds(fadeDuration + timeToShowHouse);
        playingIntro = true;
    }

    void Start()
    {
        if(!skipIntro)
        {

            StartCoroutine(Intro());
        }
        else
        {
            introCamera.SetActive(false);
            playerCamera.SetActive(true);
            playerController.enabled = true;
            fade.color = new Color(0, 0, 0, 0);
            goalText.color = new Color(0, 0, 0, 0);
        }
    }

    void Update()
    {

        if (fading)
        {
            if (fadeTimer < fadeDuration)
            {
                fadeTimer += Time.deltaTime;

                float alpha = Mathf.Lerp(1, 0, fadeTimer / fadeDuration);
                fade.color = new Color(0, 0, 0, alpha);
                goalText.color = new Color(1, 1, 1, alpha);
            }
        }

        if (fadeReverse)
        {
            if (fadeTimer < fadeDuration)
            {
                fadeTimer += Time.deltaTime;

                float alpha = Mathf.Lerp(0, 1, fadeTimer / fadeDuration);
                fade.color = new Color(0, 0, 0, alpha);
                goalText.color = new Color(1, 1, 1, alpha);
            }
        }

        if (!playingIntro || points.Length <= 0) return;

        if(currentIntroSpeed < maxIntroSpeed)
        {
            currentIntroSpeed += introAcceleration * Time.deltaTime;
        }

        Transform target = points[currentPoint];
        introCamera.transform.position = Vector3.MoveTowards(introCamera.transform.position, target.position, currentIntroSpeed * Time.deltaTime);

        if (Vector3.Distance(introCamera.transform.position, target.position) < 0.1f)
        {
            currentPoint++;

            if (currentPoint >= points.Length)
            {
                playingIntro = false;
                StartCoroutine(EndCinematic());
            }
        }
    }

    IEnumerator EndCinematic()
    {
        goalText.gameObject.SetActive(false);
        fading = false;
        fadeTimer = 0;
        fadeReverse = true;
        introCamera.SetActive(false);
        playerCamera.SetActive(true);
        yield return new WaitForSeconds(fadeDuration + 1f);
        fadeReverse = false;
        fadeTimer = 0;
        fading = true;
        yield return new WaitForSeconds(fadeDuration);
        playerController.enabled = true;
    }
}
