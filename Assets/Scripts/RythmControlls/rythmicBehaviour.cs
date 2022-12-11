using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rythmicBehaviour : MonoBehaviour
{
    float timeOfHit = 0;

    public virtual void onBeatUpdate()
    {

    }
    
    public virtual void onBeat()
    {

    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            HandleKeys();
        }
    }

    public virtual void HandleKeys()
    {
        timeOfHit = Time.time;
        float distFromBeat = Mathf.Min(
            Mathf.Abs(timeOfHit - rythmeCounter._Counter.lastBeat),
            Mathf.Abs(timeOfHit - rythmeCounter._Counter.nextBeat)
            );
        if (distFromBeat < rythmeCounter._Counter.forgiveness)
        {
            onBeat();
        }
        else
        {
            print("Offbeat");
        }
    }
}
