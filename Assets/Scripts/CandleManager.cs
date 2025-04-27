using UnityEngine;
using UnityEngine.Events;

public class CandleManager : MonoBehaviour
{
    [SerializeField] UnityEvent[] litEvents;
    [SerializeField] UnityEvent[] unlitEvents;
    [SerializeField] int maxCandles = 1;

    int currentCandles = 0;

    public void CandleLit()
    {
        currentCandles++;
        if(currentCandles >= maxCandles)
        {
            foreach(UnityEvent litEvent in litEvents)
            {
                litEvent.Invoke();
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
