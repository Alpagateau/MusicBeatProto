using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjectAlive : MonoBehaviour
{

    public int p1=0,p2=0;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
