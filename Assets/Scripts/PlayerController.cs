using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerInputAction action;
    private InputAction move;
    private InputAction fire;
    private Vector2 moveDir;
    private Vector2 lastMove;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        action = new PlayerInputAction();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        move = action.Player.Movement;
        move.Enable();

        fire = action.Player.Fire;
        fire.performed += OnFire;
        fire.Enable();
    }

    private void OnDisable()
    {
        move.Disable();

        fire.performed -= OnFire;
        fire.Disable();
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Attack Started");
            anim.SetTrigger("isAttack");
        }
    }

    private void AnimationHandle()
    {
        // Animation
        if (moveDir != Vector2.zero)
        {
            lastMove = moveDir;
            anim.SetBool("isMove", true);
            anim.SetFloat("moveX", moveDir.x);
            anim.SetFloat("moveY", moveDir.y);
        }
        else
        {
            anim.SetBool("isMove", false);
            anim.SetFloat("idleX", lastMove.x);
            anim.SetFloat("idleY", lastMove.y);
        }
    }

    private void FlipSprite()
    {

        if (lastMove.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (lastMove.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Update()
    {
        moveDir = move.ReadValue<Vector2>();
        // Animation
        AnimationHandle();
        // Flip Player
        FlipSprite();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
