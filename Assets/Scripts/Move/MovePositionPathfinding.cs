using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionPathfinding : MonoBehaviour, IMovePosition
{

    Vector3[] path;
    private int targetIndex = -1;
    private IMoveVelocity iMoveVelocity;

    private void Awake()
    {
        iMoveVelocity = GetComponent<IMoveVelocity>();
    }
    public void SetMovePosition(Vector3 movePosition)
    {
        PathRequestManager.RequestPath(transform.position, movePosition, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;

        }
    }

    private void Update()
    {
        if (targetIndex != -1 && path.Length > 0)
        {
            Vector3 nextPathPosition = path[targetIndex];
            Vector3 moveVelocity = (nextPathPosition - transform.position).normalized;
            iMoveVelocity.SetVelocity(moveVelocity);
      //      Debug.Log(targetIndex);
             if(Vector3.Distance(transform.position, nextPathPosition) < 1f)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    targetIndex = -1;
                }
            }
        }
        else
        {
            iMoveVelocity.SetVelocity(Vector3.zero);
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one * 2);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

}

