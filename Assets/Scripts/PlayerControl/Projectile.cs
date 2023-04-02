using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using music;

public class Projectile : rythmicBehaviour
{
    public Vector2 Direction;
    public Vector3 lastPosition;
    public int throwerID;
    private int previousValue;
    public override void onBeatUpdate(KeyPress[] pressed)
    {
        checkCollision();
        lastPosition = transform.position;
        transform.Translate(Direction);
        base.onBeatUpdate(pressed);
    }
    public void checkCollision()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Direction, 1);
        if(hit2D.collider != null)
        {
            Player p = hit2D.collider.GetComponent<Player>();
            int itjustworks = hit2D.collider.GetInstanceID();
            if (itjustworks >= previousValue)
            {
                p.hpc.TakeDamage(10);
                print("Damages to " + p.gameObject.name + " with 10");
                Destroy(this.gameObject);
            }
            previousValue = itjustworks;
        }
    }
}
