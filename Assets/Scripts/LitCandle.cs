using UnityEngine;

public class LitCandle : MonoBehaviour
{
    [SerializeField] Light pointLight;

    bool isLit = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isLit)
        {
            pointLight.intensity = 10f;
            isLit = true;
        }
    }
}
