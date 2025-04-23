using UnityEngine;
using UnityEngine.Events;

public class LitCandle : MonoBehaviour
{
    [SerializeField] Light pointLight;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] AudioSource audioSource;
    [SerializeField] CandleManager manager;


    bool isLit = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isLit)
        {
            pointLight.intensity = 5f;
            isLit = true;

            Material[] materials = meshRenderer.materials;

            Color fireColor = new Color(1, 0.4f, 0f) * Mathf.LinearToGammaSpace(5f);
            materials[1].SetColor("_EmissionColor", fireColor);
            materials[1].EnableKeyword("_EMISSION");

            audioSource.Play();

            manager.CandleLit();
        }
    }
}
