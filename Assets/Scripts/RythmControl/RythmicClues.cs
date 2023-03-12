using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using music;

[RequireComponent(typeof(rythmeCounter))]
public class RythmicClues : MonoBehaviour
{
    public GameObject sampleTicker;
    public RectTransform startL, startR, end, parent;

    [Range(1, 10)]
    public int nbTicker;

    Queue<GameObject> tickersR, tickersL; 

    private void Start()
    {
        rythmeCounter._Counter.beat += SpawnTicker;
        tickersL = new Queue<GameObject>();
        tickersR = new Queue<GameObject>();
    }

    public void SpawnTicker(KeyPress[] a)
    {

        //_____LEFT_____
        GameObject gm = Instantiate(sampleTicker, startL.anchoredPosition3D, Quaternion.identity, parent);
        //ça marche pas de base
        gm.GetComponent<RectTransform>().anchoredPosition3D = startL.anchoredPosition3D;
        TickerBehaviour tb = gm.GetComponent<TickerBehaviour>();
        tickersL.Enqueue(gm);

        tb.endPosition = end;
        tb.ticks_restants = nbTicker;

        //_____RIGHT_____
        GameObject gm2 = Instantiate(sampleTicker, startR.anchoredPosition3D, Quaternion.identity, parent);
        //ça marche pas de base
        gm2.GetComponent<RectTransform>().anchoredPosition3D = startR.anchoredPosition3D;
        
        TickerBehaviour tb2 = gm2.GetComponent<TickerBehaviour>();
        tickersR.Enqueue(gm2);

        tb2.endPosition = end;
        tb2.ticks_restants = nbTicker;
    }

    public void killTick(bool a, bool b)
    {
        if(a)
        {
            GameObject gm = tickersL.Peek();
            if (gm != null)
            {
                if (gm.GetComponent<TickerBehaviour>().ticks_restants == 1)
                {
                    gm.GetComponent<TickerBehaviour>().removeFromBeat();
                    Destroy(gm);
                }
            }
        }
        if(b)
        {
            GameObject gm = tickersR.Peek();
            if (gm != null)
            {
                if (gm.GetComponent<TickerBehaviour>().ticks_restants == 1)
                {
                    Destroy(gm);
                }
            }
        }
    }
}
