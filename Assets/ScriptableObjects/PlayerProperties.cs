using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NewPlayerProperty", order = 1)]
public class PlayerProperties : ScriptableObject
{
	public KeyCode[] keyCodes =
    {
        KeyCode.Z,
        KeyCode.Q,
        KeyCode.S,
        KeyCode.D
    };
}
