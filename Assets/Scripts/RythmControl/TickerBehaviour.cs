using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using music;

[RequireComponent(typeof(RectTransform))]
public class TickerBehaviour : MonoBehaviour
{
    public int ticks_restants;
    public RectTransform endPosition;

    Vector3 lastTickPosition;
    RectTransform rectTransform;

    private void Start()
    {
        rythmeCounter._Counter.beat += OnBeat;
        rectTransform = GetComponent<RectTransform>();
        lastTickPosition = rectTransform.anchoredPosition3D;
    }

    public void removeFromBeat()
    {
        rythmeCounter._Counter.beat -= OnBeat;
    }

    public void OnBeat(KeyPress[] a)
    {
        lastTickPosition = lastTickPosition + (endPosition.anchoredPosition3D - lastTickPosition) / ticks_restants;
        ticks_restants--;
        if(ticks_restants == 0)
        {
            removeFromBeat();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CalculatePosition();
    }

    public void CalculatePosition()
    {
        Vector3 nxtPos = new Vector3();
        //some vector math yk
        Vector3 dir = endPosition.anchoredPosition3D - lastTickPosition;
        nxtPos = (dir / ticks_restants) + lastTickPosition;

        Vector3 newPos = ((dir / ticks_restants) * rythmeCounter._Counter.rythmicScaledTime) + lastTickPosition;
        rectTransform.anchoredPosition3D = newPos;
    }
}
