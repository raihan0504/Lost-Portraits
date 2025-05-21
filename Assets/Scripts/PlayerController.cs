using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerInputAction action;
    private SpriteRenderer spriteRenderer;

    [Header("InputAction")]
    private InputAction move;
    private InputAction fire;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] GameObject attackZone;
    private bool isAttacking;
    private Vector2 lastMove;
    private Vector2 moveDir;

    private void Start()
    {
        if (attackZone != null)
            attackZone.SetActive(false);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        action = new PlayerInputAction();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

            // Aktifkan area serangan sementara
            StartCoroutine(DoAttack());
        }
    }

    private IEnumerator DoAttack()
    {
        isAttacking = true;
        attackZone.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        attackZone.SetActive(false);
        isAttacking = false;
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
            spriteRenderer.flipX = false;
        } else if(lastMove.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void UpdateAttackZoneDir()
    {
        if (lastMove == Vector2.zero) return;

        // Normalisasi ke arah delapan arah (tanpa diagonal jika mau)
        Vector2 direction = lastMove;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction = new Vector2(Mathf.Sign(direction.x), 0); // Horizontal
        else
            direction = new Vector2(0, Mathf.Sign(direction.y)); // Vertikal

        // Geser AttackZone ke arah yang sesuai
        attackZone.transform.localPosition = direction;
    }

    private void UseItem()
    {
        Item item = FindObjectOfType<InventoryManager>().GetSelectedItem(true);
        if (item != null)
        {
            Debug.Log($"Using item: {item.name}, ActionType: {item.actionType}");

            switch (item.actionType)
            {
                case ActionType.Heal:
                    GetComponent<Health>()?.Heal(item.healAmount); // kamu bisa sesuaikan angka 20 ini atau bikin variabel di SO
                    break;

                case ActionType.Attack:
                    // Bisa kasih efek khusus nanti
                    Debug.Log("Attack item used");
                    break;

                case ActionType.Unlock:
                    Debug.Log("Key used");
                    break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseItem();
        }

        moveDir = move.ReadValue<Vector2>();
        // Animation
        AnimationHandle();
        // Flip Player
        FlipSprite();

        UpdateAttackZoneDir();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
