using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : rythmicBehaviour
{
    public Vector2 Direction;

    public override void onBeatUpdate()
    {
        transform.Translate(Direction);
        base.onBeatUpdate();
    }
}
