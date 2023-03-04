using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace music
{
    public enum RythmicStatus
    {
        Offbeat,
        Ok,
        Perfect
    }

    public struct KeyPress
    {
        public RythmicStatus status;
        public float score;
        public KeyCode key;
    }
}

