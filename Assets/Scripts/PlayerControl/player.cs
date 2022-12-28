using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : rythmicBehaviour
{
    public bool isGrounded;
    public LayerMask groundMask;
    public player otherPlayer;
    public Projectile Bullet;
    public KeyCode[] keyCodes =
    {
        KeyCode.Z,
        KeyCode.Q,
        KeyCode.S,
        KeyCode.D
    };

    public enum action
    {
        none,
        right,
        left,
        jump,
        shoot
    };

    action nextAction = action.none;
    public action lastAction;

    public Vector3 nextPos;

    public override void HandleKeys()
    {
        if (Input.GetKeyDown(keyCodes[3]))
        {
            nextAction = action.right;
        }
        if (Input.GetKeyDown(keyCodes[1]))
        {
            nextAction = action.left;
        }
        if (Input.GetKeyDown(keyCodes[0]))
        {
            nextAction = action.jump;
        }
        if (Input.GetKeyDown(keyCodes[2]))
        {
            nextAction = action.shoot;
        }
        base.HandleKeys();
    }

    public override void onBeat()
    {
        //check grounded
    }

    public override void onBeatUpdate()
    {
        Vector3 mov = Vector3.zero;

        Debug.DrawRay(transform.position, -Vector2.up, Color.red, 1.0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.0f, groundMask);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (!isGrounded)
        {
            if (nextAction != action.jump)
            {
                mov = new Vector3(0, -1, 0);
                print("gravity");
            }
        }

        if (nextAction == action.right)
        {
            mov = new Vector3(1, 0, 0);
        }
        if (nextAction == action.left)
        {
            mov = new Vector3(-1, 0, 0);
        }
        if (nextAction == action.jump)
        {
            mov = new Vector3(0, 2, 0);
            print("jump");
        }
        if (nextAction == action.shoot)
        {
            Shoot();
        }
        if (nextAction != action.none)
            lastAction = nextAction;
        nextAction = action.none;
        //this part will handle collisions
        nextPos = new Vector3(mov.x + (int)transform.position.x, mov.y + (int)transform.position.y);
        if(nextPos == otherPlayer.transform.position && otherPlayer.nextPos == transform.position)
        {
            //can't move
            print("can't move");
        }
        else if(nextPos ==  otherPlayer.nextPos)
        {
            //deflect
            print("deflect actulaly");
        }
        else
        {
            transform.Translate(mov);
        }
        base.onBeatUpdate();
    }

    public void Shoot()
    {
        Projectile bullet = GameObject.Instantiate(Bullet, transform.position, Quaternion.identity);
        bullet.Direction = Vector2.right;
        if (lastAction == action.left)
        {
            bullet.Direction = Vector2.left;
        }
        else if (lastAction == action.jump)
        {
            bullet.Direction = Vector2.up;
        }
    }
}
