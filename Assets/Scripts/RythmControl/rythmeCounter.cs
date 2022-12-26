using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rythmeCounter : MonoBehaviour
{
    //BPM Beat Per Minutes
    [Range(60.0f,200.0f)]
    public float BPM = 130.0f;

    [Tooltip("Hit Per Beat")]
    [Range(1,4)]
    //Hits per beat
    public int HPB = 1;

    //time btw the closest beat and the furthest accepted input action
    [Range(0.0f, 2.0f)]
    public float forgiveness = 0.2f;

    public AudioSource musicSource;

    public Image rythmeHelperPlaceHolder, rythmeF1, rythmeF2;

    public static rythmeCounter _Counter;
    
    [HideInInspector]
    public float lastBeat = 0;
    [HideInInspector]
    public float lastUpdate = 0; //on beat updates are just after the forgiveness threshold
    [HideInInspector]
    public float nextBeat = 0;

    //just to save the value so that we compute it only once (Beat Per Seconde)
    float bps = 0;
    float btwbps = 0;

    rythmicBehaviour[] rythmics;

    private void Awake()
    {
        if(_Counter == null)
        {
            _Counter = this;
        }
    }

    private void Start()
    {
        rythmics = FindObjectsOfType<rythmicBehaviour>();
        lastBeat = Time.time;
        bps = BPM / 60;
        btwbps = 1 / bps;
        nextBeat = lastBeat + btwbps;
        musicSource.Play();
        lastUpdate = forgiveness;
    }

    private void Update()
    {
        rythmeHelperPlaceHolder.rectTransform.rotation = Quaternion.Euler(0,0,360* (Time.time - lastBeat) / btwbps);
        if (Time.time >= btwbps + lastBeat)
        {
            lastBeat = Time.time;
            nextBeat = lastBeat + btwbps;
        }
        if (Time.time >= btwbps + lastUpdate)
        {
            foreach (rythmicBehaviour r in rythmics)
            {
                r.onBeatUpdate();
            }
            lastUpdate = Time.time;
        }
        rythmeF1.fillAmount = forgiveness / btwbps;
        rythmeF2.fillAmount = forgiveness / btwbps;
    }
}
