using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTempoController : MonoBehaviour
{
    //BPM Beat Per Minutes
    [Range(60.0f, 200.0f)]
    public float BPM = 120.0f;

    //time btw the closest beat and the furthest accepted input action
    [Range(0.5f, 1.0f)]
    public float forgiveness = 0.8f;

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

    //Envent called on each beat
    public delegate void beatDelegate();
    public beatDelegate beat;

    //envent called each beat, right after the "beat" event
    public delegate void afterBeatDelegate();
    public afterBeatDelegate onBeatUpdate;

    public enum RythmicStatus
    {
        Offbeat,
        Ok,
        Perfect
    }

    public RythmicStatus rythmicStatus;
    [Range(0f, 1f)]
    public float rythmicPosition;

    public Player player1, player2;

    private void Start()
    {
        bps = BPM / 60;
        btwbps = 1 / bps;
        lastBeat = Time.time;
        nextBeat = lastBeat + btwbps;
        musicSource.Play();
    }

    private void Update()
    {
        List<KeyCode> PressedKeys = CheckInputs();

        //Sets the Rytmic status
        CheckTempo();

        //Do Actions
        MovePlayers();
    }

    public List<KeyCode> CheckInputs()
    {
        List<KeyCode> pressed = new List<KeyCode>();
        foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(k))
            {
                pressed.Add(k);
            }
        }

        //write for special commands, like pause and stop the game
        return pressed;
    }

    public void CheckTempo()
    {
        if(Time.time > nextBeat)
        {
            lastBeat = Time.time;
            nextBeat = lastBeat + btwbps;
        }

        float scaledTime = (Time.time - lastBeat) / btwbps;
        // f(x) = 4x(x-1) + 1
        Func<float, float> distanceFunc = x => (4 * x * (x - 1)) + 1;
        float beatDistance = distanceFunc(scaledTime);
        rythmicPosition = beatDistance;
        if(beatDistance < 0.5f)
        {
            rythmicStatus = RythmicStatus.Offbeat;
        }
        else if(beatDistance < forgiveness)
        {
            rythmicStatus = RythmicStatus.Ok;
        }
        else if(beatDistance >= forgiveness)
        {
            rythmicStatus = RythmicStatus.Perfect;
        }
    }

    public void MovePlayers()
    {

    }
}
