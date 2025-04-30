using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] float minContrast = -1f;
    [SerializeField] float maxContrast = 3f;
    [SerializeField] float defaultValue = 1f;
    [SerializeField] TextMeshProUGUI  brightnessText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBrightnessValueChanged(float value)
    {
        Debug.Log(value);
        ColorAdjustments colorAdjustments;
        if (volume.profile.TryGet(out colorAdjustments))
        {
            float newValue = minContrast + (maxContrast - minContrast) * value;
            colorAdjustments.postExposure.value = newValue;
            brightnessText.text = newValue.ToString("F2");
        }
        
    }
}
