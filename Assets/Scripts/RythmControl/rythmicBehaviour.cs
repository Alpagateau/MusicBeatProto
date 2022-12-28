using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rythmicBehaviour : MonoBehaviour
{
    float timeOfHit = 0;

    private void Start()
    {
        rythmeCounter._Counter.beat += onBeatUpdate;
    }

    public virtual void onBeatUpdate()
    {

    }
    
    //handles action every frame on beat
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
