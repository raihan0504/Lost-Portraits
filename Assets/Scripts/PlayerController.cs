using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 movement;

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement.Normalize();
    }

    private void PlayerMove()
    {
        rb.velocity = movement * moveSpeed;
    }


}
