using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


[RequireComponent(typeof(BoxCollider2D))]
public class rythmicCollider : MonoBehaviour
{
    public int height = 1, width = 1;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position;
        Gizmos.DrawLine(new Vector3(pos.x - width / 2f, pos.y + height/2f, 0), new Vector3(pos.x + width / 2f, pos.y + height / 2f, 0));
        Gizmos.DrawLine(new Vector3(pos.x - width / 2f, pos.y - height / 2f, 0), new Vector3(pos.x + width / 2f, pos.y - height / 2f, 0));
        Gizmos.DrawLine(new Vector3(pos.x - width / 2f, pos.y + height / 2f, 0), new Vector3(pos.x - width / 2f, pos.y - height / 2f, 0));
        Gizmos.DrawLine(new Vector3(pos.x + width / 2f, pos.y + height / 2f, 0), new Vector3(pos.x + width / 2f, pos.y - height / 2f, 0));
    }
#endif
}
