using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Setting")]
    public Rigidbody2D rb;
    public Animator anim;
    [SerializeField] private float moveSpeed = 3f;

    private bool isFaceRight = true;
    private float inputX, inputY;
    private float idleX, idleY;
    private Vector2 movement;

    private void Update()
    {
        PlayerInput();
        PlayerAnimation();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        rb.velocity = movement * moveSpeed;
        PlayerFlipX();
    }

    private void PlayerAnimation()
    {
        anim.SetFloat("idleX", idleX);
        anim.SetFloat("idleY", idleY);
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.y);
        anim.SetBool("isMove", movement != Vector2.zero);
    }

    private void PlayerInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(inputX, inputY).normalized;
        if (movement != Vector2.zero)
        {
            idleX = inputX;
            idleY = inputY;
        }
    }

    private void PlayerFlipX()
    {
        if (movement.x > 0)
        {
            isFaceRight = true;
        } else if(movement.x < 0)
        {
            isFaceRight = false;
        }
        transform.localScale = new Vector3(isFaceRight ? 1 : -1, 1, 1);
    }

}
