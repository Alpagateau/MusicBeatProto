using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ordi : Player
{
    [Range(1, 5)]
    public int attackDist = 2;

    public GameObject mouse, mouseCable;

    GameObject[] cables; 

    private void Start()
    {
        initialize();
        //rythmeCounter._Counter.beat += onBeatUpdate;
    }

    public override void initialize()
    {
        mouse.SetActive(false);
        mouseCable.SetActive(false);
        base.initialize();
    }

    public override void Attack()
    {
        if (nextAction == actionset.locked)
        {
            mouse.SetActive(false);
            if (attackDist > 1)
            {
                for (int i = 0; i < attackDist - 1; i++)
                {
                    Destroy(cables[i]);
                }
                cables = null;
            }
            print("action unlocked");
            nextAction = actionset.none;
        }
        if (nextAction == actionset.shoot)
        {
            Vector3 mousePos = transform.position;
            float dir = (isLookingRight ? 1 : -1);
            mousePos.x += attackDist * dir;
            Vector3 newScale = new Vector3(dir, 1, 1);
            mouse.transform.localScale = newScale;
            mouse.transform.position = mousePos;
            if (attackDist > 1)
            {
                mousePos.x -= 0.5f * dir;
                cables = new GameObject[attackDist - 1];
                for(int i = 0; i<attackDist-1; i++)
                {
                    mousePos.x -= dir;
                    cables[i] = Instantiate(mouseCable, mousePos, Quaternion.identity);
                    cables[i].SetActive(true);
                }
            }
            print("action locked");
            nextAction = actionset.locked;
            mouse.SetActive(true);
        }
        //base.Attack();
    }
}