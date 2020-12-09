using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementKeys : MonoBehaviour
{
    private IMoveVelocity moveVelocity;

    private void Awake()
    {
        moveVelocity = GetComponent<IMoveVelocity>();
    }

    private void Update()
    {
        float moveX = 0;
        float moveY = 0;
        if (Input.GetKey("up")) moveY += 1f;
        if (Input.GetKey("down")) moveY -= 1f;
        if (Input.GetKey("left")) moveX -= 1f;
        if (Input.GetKey("right")) moveX += 1f;


        Vector3 moveVector = new Vector3(moveX, moveY).normalized;
        moveVelocity.SetVelocity(moveVector); 
    }
}
