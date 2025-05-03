using UnityEngine;
using UnityEngine.Events;

public class CandleManager : MonoBehaviour
{
    [SerializeField] UnityEvent[] litEvents;
    [SerializeField] UnityEvent[] unlitEvents;
    [SerializeField] int maxCandles = 1;
    [SerializeField] AudioSource successSound;

    int currentCandles = 0;

    public void CandleLit()
    {
        currentCandles++;
        if(currentCandles >= maxCandles)
        {
            foreach(UnityEvent litEvent in litEvents)
            {
                litEvent.Invoke();
                successSound.Play();
            }
        }
    }

    public void CandleUnlit()
    {
        currentCandles--;

        if(currentCandles < maxCandles)
        {
            foreach (UnityEvent unlitEvent in unlitEvents)
            {
                unlitEvent.Invoke();
            }
        }
    }

}
