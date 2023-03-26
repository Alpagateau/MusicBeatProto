using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brot : Player
{
    [Range(1, 5)]
    public int attackDist = 2;

    private void Start()
    {
        initialize();
    }

    public override void Attack()
    {
        //base.Attack();
        if(isGrounded)
        {
            Vector3 direction = new Vector3(
                (isLookingRight)?1:-1, //if isLookingRight : x = 1; else x = -1;
                0, 
                0);
            Vector3 nPos = transform.position + direction * attackDist;
            //COLISION CHECK HERE
            if(nPos == otherPlayer.transform.position)
            {
                nPos -= direction;
            }
            nextPos = nPos;
            transform.position = nPos;
        }
        nextAction = actionset.none;
    }

    //just some debug
    private void OnDrawGizmos()
    {
        Vector3 direction = new Vector3(
                (isLookingRight) ? 1 : -1, //if isLookingRight : x = 1; else x = -1;
                0,
                0);
        Vector3 nPos = transform.position + direction * attackDist;
        //COLISION CHECK HERE
        if (nPos == otherPlayer.transform.position)
        {
            nPos -= direction;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawCube(nPos, Vector3.one);
    }
}

