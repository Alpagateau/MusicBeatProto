using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using music;

public class rythmeCounter : MonoBehaviour
{

    //BPM Beat Per Minutes
    [Range(60.0f, 200.0f)]
    public float BPM = 120.0f;

    //time btw the closest beat and the furthest accepted input action
    [Range(0f, 1.0f)]
    public float forgiveness = 0.7f;
    [Range(0, 1.0f)]
    public float offbeatThreshold = 0.3f;

    public AudioSource musicSource;

    [HideInInspector]
    public float lastBeat = 0;
    [HideInInspector]
    public float nextBeat = 0;

    //just to save the value so that we compute it only once (Beat Per Seconde)
    [HideInInspector]
    public float bps = 0;
    [HideInInspector]
    public float btwbps = 0;

    //Envent called on each beat (exactly on the frame of the 
    public delegate void beatDelegate(KeyPress[] pressed);
    public beatDelegate beat;

    //envent called each beat, right after the "beat" event
    public delegate void afterBeatDelegate(KeyPress[] pressed);
    public afterBeatDelegate afterBeat;

    public RythmicStatus rythmicStatus;
    [Range(0f, 1f)]
    public float rythmicPosition;
    //a float form 0 to 1 with 0 and 1 being perfect beats and 0.5 being offbeat
    public float rythmicScaledTime;

    public Player player1, player2;

    public static rythmeCounter _Counter;

    public bool debuggerBeats;

    [HideInInspector]
    List<KeyPress> PressedKeys;

    RythmicClues rythmicClues;

    private void Awake()
    {
        if (_Counter == null)
            _Counter = this;
    }

    private void Start()
    {
        rythmicClues = GetComponent<RythmicClues>();

        bps = BPM / 60;
        btwbps = 1 / bps;
        lastBeat = Time.time;
        nextBeat = lastBeat + btwbps;
        musicSource.Play();
        PressedKeys = new List<KeyPress>();
    }

    private void Update()
    {
        //Sets the Rytmic status
        CheckTempo();
        
        //Check all inputs
        CheckInputs();

        //Do Actions
        MovePlayers();
    }

    public void CheckInputs()
    {
        List<KeyCode> pressed = new List<KeyCode>();
        foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(k))
            {
                KeyPress p;
                p.status = rythmicStatus;
                p.score = rythmicPosition;
                p.key = k;
                PressedKeys.Add(p);
                pressed.Add(k);
            }
        }

        //write for special commands, like pause and stop the game
        if(pressed.Contains(KeyCode.Space))
        {
            print(rythmicStatus.ToString() + "||" + rythmicPosition);
        }
    }

    public void CheckTempo()
    {
        KeyPress[] keyPresses = PressedKeys.ToArray();
        //print(keyPresses.Length + " == lenght of pressed");

        RythmicStatus frameStatus = rythmicStatus;
        if (!debuggerBeats)
        {
            if (Time.time > nextBeat)
            {
                lastBeat = Time.time;
                nextBeat = lastBeat + btwbps;
                beat.Invoke(keyPresses);
            }
        }
        float scaledTime = 0;
        if (!debuggerBeats)
        {
            scaledTime = (Time.time - lastBeat) / btwbps;
            rythmicScaledTime = scaledTime;
        }
        else
        {
            scaledTime = rythmicScaledTime;
        }
        // f(x) = 4x(x-1) + 1
        Func<float, float> distanceFunc = x => (4 * x * (x - 1)) + 1;
        float beatDistance = distanceFunc(scaledTime);
        rythmicPosition = beatDistance;

        if (beatDistance < offbeatThreshold)
        {
            rythmicStatus = RythmicStatus.Offbeat;
        }
        else if (beatDistance < forgiveness)
        {
            rythmicStatus = RythmicStatus.Ok;
        }
        else if (beatDistance >= forgiveness)
        {
            rythmicStatus = RythmicStatus.Perfect;
        }

        if (!debuggerBeats)
        {
            //passe de OK a OffBeat (fin de la zone interactive)
            if (rythmicStatus == RythmicStatus.Offbeat && frameStatus == RythmicStatus.Ok)
            {
                afterBeat(keyPresses);
                PressedKeys.Clear();
            }
        }

    }

    public void MovePlayers()
    {
        //Checks also for ticks
        bool hitPlayer1 = player1.AnyKeyPressed(PressedKeys.ToArray());
        bool hitPlayer2 = player2.AnyKeyPressed(PressedKeys.ToArray());

        rythmicClues.killTick(hitPlayer1, hitPlayer2);
    }

    private void OnGUI()
    {
        if (debuggerBeats)
        {
            if (GUI.Button(new Rect(10, 10, 100, 25), "beat"))
            {
                beat(new KeyPress[1]);
            }
            if (GUI.Button(new Rect(10, 40, 100, 25), "afterBeat"))
            {
                beat(new KeyPress[1]);
            }
            rythmicScaledTime = GUI.HorizontalSlider(
                new Rect(10, 70, 100, 10),
                rythmicScaledTime,
                0.00f,
                1.00f
                );
            GUI.Box(new Rect(10, 90, 100, 25), rythmicStatus.ToString());
        }
    }
}
