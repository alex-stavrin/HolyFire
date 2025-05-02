using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DetectPerformanceAndSetQuality());
    }

    private System.Collections.IEnumerator DetectPerformanceAndSetQuality()
    {
        int frames = 0;
        float timer = 0f;

        yield return null;

        while (timer < 1f)
        {
            frames++;
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        float fps = frames / timer;

        if (fps >= 90)
            QualitySettings.SetQualityLevel(5, true); // Ultra
        else if (fps >= 60)
            QualitySettings.SetQualityLevel(3, true); // High
        else if (fps >= 30)
            QualitySettings.SetQualityLevel(2, true); // Medium
        else
            QualitySettings.SetQualityLevel(0, true); // Low
    }
}
