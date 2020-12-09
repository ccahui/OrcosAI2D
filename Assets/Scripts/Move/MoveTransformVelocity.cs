using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private float moveSpeed;
    private Vector3 velocity;
   
    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += this.velocity * moveSpeed * Time.deltaTime;
    }
}
