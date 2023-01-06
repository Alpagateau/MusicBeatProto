using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rythmicBehaviour : MonoBehaviour
{
    float timeOfHit = 0;
    public bool isOnBeat = false;

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
        GameObject obj = GameObject.Instantiate(rythmeCounter._Counter.rythmeHelperClue, 
            rythmeCounter._Counter.rythmeHelperPlaceHolder.transform.position, 
            rythmeCounter._Counter.rythmeHelperPlaceHolder.transform.rotation);
        obj.transform.SetParent(rythmeCounter._Counter.rythmeHelperPlaceHolder.transform.parent, false);
        Destroy(obj, 0.5f);

        timeOfHit = Time.time;
        float distFromBeat = Mathf.Min(
            Mathf.Abs(timeOfHit - rythmeCounter._Counter.lastBeat),
            Mathf.Abs(timeOfHit - rythmeCounter._Counter.nextBeat)
            );
        if (distFromBeat < rythmeCounter._Counter.forgiveness)
        {
            isOnBeat = true;
        }
        else
        {
            isOnBeat = false;
        }
    }

    public void OnDestroy()
    {
        rythmeCounter._Counter.beat -= onBeatUpdate;
        Destroy(gameObject);
    }
}
