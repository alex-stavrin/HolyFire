using UnityEngine;

public class EggPlatform : MonoBehaviour
{
    [SerializeField] CandleManager candleManager;
    [SerializeField] EggColor color;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Egg")
        {
            Egg egg = other.GetComponent<Egg>();
            if(egg)
            {
                if(egg.color == color)
                {
                    candleManager.CandleLit();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Egg")
        {
            Egg egg = other.GetComponent<Egg>();
            if (egg)
            {
                if (egg.color == color)
                {
                    candleManager.CandleUnlit();
                }
            }
        }
    }
}