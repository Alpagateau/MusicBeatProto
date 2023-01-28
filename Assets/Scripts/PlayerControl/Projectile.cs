using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : rythmicBehaviour
{
    public Vector2 Direction;
    public Vector3 lastPosition;
    public int throwerID;

    public override void onBeatUpdate()
    {
        checkCollision();
        lastPosition = transform.position;
        transform.Translate(Direction);
        base.onBeatUpdate();
    }

    public void checkCollision()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Direction, 1);
        if(hit2D.collider != null)
        {
            player p = hit2D.collider.GetComponent<player>();
            if(p != null)
            {
                if (p.playerID != throwerID)
                {
                    p.hpc.TakeDamage(10);
                    print("Damages to " + p.gameObject.name + " with 10");
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
