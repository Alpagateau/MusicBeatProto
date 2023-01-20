using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : rythmicBehaviour
{
    [Header("=> Player Caracteristics")]
    [Tooltip("Is the player touching the ground")]
    public bool isGrounded;

    [Tooltip("what is considered the ground")]
    public LayerMask groundMask;

    [Tooltip("Is the sprite looking right")]
    public bool isLookingRight;

    [Tooltip("The identifier of the chosen Sprite")]
    public int playerType;

    [Tooltip("The identifier for each player, can only be 1 or 2")]
    public int playerID;

    public KeepObjectAlive K;

    [Header("=> References")]
    [Tooltip("A reference to the other player")]
    public player otherPlayer;

    [Tooltip("The prefab of the bullet")]
    public Projectile bulletPrefab;

    public SpriteRenderer _spriteRenderer;

    [Header("=> Controls")]
    [Tooltip("up; left; down; right. Ex : Z Q S D")]
    public KeyCode[] keyCodes =
    {
        KeyCode.Z,
        KeyCode.Q,
        KeyCode.S,
        KeyCode.D
    };

    public enum actionset
    {
        none,
        right,
        left,
        jump,
        shoot,
        locked
    };

    [HideInInspector]
    public actionset nextAction = actionset.none;
    [HideInInspector]
    public actionset lastAction;
    [HideInInspector]
    public Vector3 nextPos;


    private void Start()
    {
        rythmeCounter._Counter.beat += onBeatUpdate;
        K = (KeepObjectAlive)FindObjectOfType(typeof(KeepObjectAlive));
        print(K.p1);
    }

    public override void HandleKeys()
    {
        base.HandleKeys();
        if (nextAction != actionset.locked)
        {
            if (!isOnBeat)
            {
                nextAction = actionset.none;
                return;
            }
            if (Input.GetKeyDown(keyCodes[3]))
            {
                nextAction = actionset.right;
            }
            if (Input.GetKeyDown(keyCodes[1]))
            {
                nextAction = actionset.left;
            }
            if (Input.GetKeyDown(keyCodes[0]))
            {
                nextAction = actionset.jump;
            }
            if (Input.GetKeyDown(keyCodes[2]))
            {
                nextAction = actionset.shoot;
                print("action = shoot");
            }
        }
    }

    public override void onBeatUpdate()
    {
        print("onBeatUpdateIsCalled");
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
            if (nextAction != actionset.jump)
            {
                mov = new Vector3(0, -1, 0);
                print("gravity");
            }
        }

        if (nextAction == actionset.locked)
        {
            Attack();
            print("shoot 2 is called");
        }
        if (nextAction == actionset.right)
        {
            mov = new Vector3(1, 0, 0);
            isLookingRight = true;
        }
        if (nextAction == actionset.left)
        {
            mov = new Vector3(-1, 0, 0);
            isLookingRight = false;
        }
        if (nextAction == actionset.jump)
        {
            mov = new Vector3(0, 2, 0);
            print("jump");
        }
        if (nextAction == actionset.shoot)
        {
            Attack();
            print("shoot 1 is called");
        }
        if (nextAction != actionset.none)
            lastAction = nextAction;
        if(nextAction != actionset.locked)
            nextAction = actionset.none;
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
            print("deflect actually");
        }
        else
        {
            transform.Translate(mov);
        }
        _spriteRenderer.flipX = !isLookingRight;
        base.onBeatUpdate();
    }

    public virtual void Attack()
    {
        Projectile bullet = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.Direction = Vector2.right;
        if (lastAction == actionset.left)
        {
            bullet.Direction = Vector2.left;
        }
        else if (lastAction == actionset.jump)
        {
            bullet.Direction = Vector2.up;
        }
        Destroy(bullet, 10);
    }
}