using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject[] playerTypes;
    public KeepObjectAlive ke;

    private void Start()
    {
        GameObject a = GameObject.Instantiate(playerTypes[ke.p1-1],new Vector3(-3,0,0),Quaternion.identity);
        GameObject b = GameObject.Instantiate(playerTypes[ke.p2-1],new Vector3(3,0,0),Quaternion.identity);
    }
}
