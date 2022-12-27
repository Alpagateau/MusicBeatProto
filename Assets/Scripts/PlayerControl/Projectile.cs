using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : rythmicBehaviour
{
    public Vector2 Direction;

    public override void onBeatUpdate()
    {
        /*Vector3 oldPos = transform.position;
        oldPos.x += Direction.x;
        oldPos.y += Direction.y;
        transform.position = oldPos;*/
        transform.Translate(Direction);
        base.onBeatUpdate();
    }
}
