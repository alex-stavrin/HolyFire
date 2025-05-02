using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] float minContrast = -1f;
    [SerializeField] float maxContrast = 3f;
    [SerializeField] TextMeshProUGUI  brightnessText;
    [SerializeField] Slider brightnessSlider;
    [SerializeField] Image fade;
    [SerializeField] float fadeDuration;

    bool isFading = false;
    float fadeTimer = 0;

    void Start()
    {
        OnBrightnessValueChanged(0.4375f);
    }

    private void Update()
    {
        if(isFading)
        {
            fadeTimer += Time.deltaTime;

            float alpha = Mathf.Lerp(0,1, fadeTimer / fadeDuration);
            fade.color = new Color(0, 0, 0, alpha);

            if(fadeTimer > fadeDuration + 0.5)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void OnBrightnessValueChanged(float value)
    {
        ColorAdjustments colorAdjustments;
        if (volume.profile.TryGet(out colorAdjustments))
        {
            float newValue = minContrast + (maxContrast - minContrast) * value;
            colorAdjustments.postExposure.value = newValue;
            brightnessText.text = newValue.ToString("F2");
        }
    }

    public void ResetToDefault()
    {
        brightnessSlider.value = 0.4375f;
    }

    public void GoToLevel()
    {
        isFading = true;
    }
}
