using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using music;

public class rythmicBehaviour : MonoBehaviour
{
    public bool isOnBeat = false;

    private void Start()
    {
        rythmeCounter._Counter.beat += onBeatUpdate;
    }

    public virtual void onBeatUpdate(KeyPress[] pressed)
    {

    }
    
    //handles action every frame on beat
    public virtual void onBeat()
    {

    }

    public virtual void HandleKeys(KeyPress[] pressed)
    {
        
    }

    public void OnDestroy()
    {
        rythmeCounter._Counter.beat -= onBeatUpdate;
        Destroy(gameObject);
    }
}
