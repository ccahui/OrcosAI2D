using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private float moveSpeed;
    private Vector3 velocity;
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidbody2D.velocity = this.velocity * moveSpeed;
    }
}
