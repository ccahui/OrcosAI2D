using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionDirect : MonoBehaviour, IMovePosition
{
    private IMoveVelocity move;
    private Vector3 movePosition;
    private void Awake()
    {
        move = GetComponent<IMoveVelocity>();
        movePosition = transform.position;
    }
    public void SetMovePosition(Vector3 position)
    {
        this.movePosition = position;
    }

    // Update is called once per frame
    private void Update()
    {
    

     if (Vector3.Distance(movePosition, transform.position) > 1f)
        {
            Vector3 moveDir = (movePosition - transform.position).normalized;
            GetComponent<IMoveVelocity>().SetVelocity(moveDir);
            move.SetVelocity(moveDir);
        }
        else
        {
            move.SetVelocity(Vector3.zero);
        }

    }
}
