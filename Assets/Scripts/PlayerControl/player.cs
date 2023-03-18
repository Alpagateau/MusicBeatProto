using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using music;

public class Player : rythmicBehaviour
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

    [HideInInspector]
    public healthControl hpc;

    public KeepObjectAlive K;

    [Header("=> References")]
    [Tooltip("A reference to the other player")]
    public Player otherPlayer;

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
        initialize();
    }

    public virtual void initialize()
    {
        rythmeCounter._Counter.beat += onBeatUpdate;
        rythmeCounter._Counter.afterBeat += HandleKeys;
        K = (KeepObjectAlive)FindObjectOfType(typeof(KeepObjectAlive));
        if (K != null)
            print(K.p1);
        hpc = GetComponent<healthControl>();
    }

    public override void HandleKeys(KeyPress[] pressed)
    {
        base.HandleKeys(pressed);
        if (nextAction != actionset.locked)
        {
            
            foreach(KeyPress k in pressed)
            {
                if (k.status == RythmicStatus.Ok || k.status == RythmicStatus.Perfect)
                {
                    if (keyCodes[3] == k.key)
                    {
                        nextAction = actionset.right;
                    }
                    if (keyCodes[1] == k.key)
                    {
                        nextAction = actionset.left;
                    }
                    if (keyCodes[0] == k.key)
                    {
                        nextAction = actionset.jump;
                    }
                    if (keyCodes[2] == k.key)
                    {
                        nextAction = actionset.shoot;
                    }
                }
            }
        }
    }

    public override void onBeatUpdate(KeyPress[] pressed)
    {
        //print("onBeatUpdateIsCalled");
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
                print(name + " || gravity");
            }
        }

        if (nextAction == actionset.locked)
        {
            Attack();
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
            print(name + "|| jump");
        }
        if (nextAction == actionset.shoot)
        {
            Attack();
            print(name + " || shoot 1 is called");
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
            print(name + " || can't move");
        }
        else if(nextPos ==  otherPlayer.nextPos)
        {
            //deflect
            print(name + " || deflect actually");
        }
        else
        {
            transform.Translate(mov);
        }
        _spriteRenderer.flipX = !isLookingRight;
        base.onBeatUpdate(pressed);
    }

    public virtual void Attack()
    {
        Projectile bullet = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.throwerID = playerID;
        bullet.Direction = isLookingRight?Vector2.right:Vector2.left;
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

    public bool AnyKeyPressed(KeyPress[] pressed)
    {
        for(int i = 0; i< pressed.Length; i++)
        {
            for(int j = 0; j<keyCodes.Length; j++)
            {
                if(keyCodes[j] == pressed[i].key)
                {
                    return true;
                }
            }
        }
        return false;
    }
}