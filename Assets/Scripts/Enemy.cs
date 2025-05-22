using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] waypoints;
    public float patrolSpeed = 2f;
    private int currentWaypointIndex = 0;

    [Header("Chase & Attack")]
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float chaseSpeed = 3.5f;
    public int damage = 20;
    public float attackCooldown = 0.5f;

    [Header("Knockback Settings")]
    public float knockbackForce = 30f; // Tingkatkan knockback force
    public float staggerDuration = 0.5f; // Durasi stagger lebih lama untuk efek knockback terlihat

    [Header("Components")]
    private Transform player;
    private Rigidbody2D rb;
    private float lastAttackTime;

    private enum State { Patrol, Chase, Attack, Staggered }
    private State currentState = State.Patrol;

    private bool isStaggered = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Memastikan settings Rigidbody2D optimal untuk knockback
        if (rb != null)
        {
            // Mass optimal untuk knockback
            if (rb.mass > 1f)
            {
                rb.mass = 1f;
                Debug.Log("Enemy mass adjusted to 1.0");
            }

            // Drag rendah agar knockback bisa bertahan lebih lama
            if (rb.drag > 0.5f)
            {
                rb.drag = 0.5f;
                Debug.Log("Enemy drag adjusted to 0.5");
            }
        }
    }

    private void Update()
    {
        // PENTING: Jangan reset velocity saat staggered!
        if (isStaggered)
        {
            // Biarkan physics bekerja tanpa diintervensi
            return;
        }

        if (player == null)
        {
            currentState = State.Patrol;
            HandleState();
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Tentukan state berdasarkan jarak
        if (distanceToPlayer <= attackRange)
            currentState = State.Attack;
        else if (distanceToPlayer <= detectionRange)
            currentState = State.Chase;
        else
            currentState = State.Patrol;

        HandleState();
    }

    private void HandleState()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                ChasePlayer();
                break;
            case State.Attack:
                AttackPlayer();
                break;
            case State.Staggered:
                // Dalam state stagger, biarkan physics bekerja
                break;
        }
    }

    private void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector2 dir = (targetWaypoint.position - transform.position).normalized;
        rb.velocity = dir * patrolSpeed;

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        FlipSprite(dir.x);
    }

    private void ChasePlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = dir * chaseSpeed;

        FlipSprite(dir.x);
    }

    private void AttackPlayer()
    {
        rb.velocity = Vector2.zero;

        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;

            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth != null)
                {
                    Vector2 knockbackDir = (player.position - transform.position).normalized;
                    playerHealth.TakeDamage(damage, knockbackDir);
                }
            }
        }
    }

    public void Stagger(Vector2 knockbackDir)
    {
        StopAllCoroutines(); // Hindari tumpang tindih coroutine
        isStaggered = true;
        currentState = State.Staggered;

        // Reset velocity SEBELUM knockback
        rb.velocity = Vector2.zero;

        // Tambah gaya knockback - gunakan force yang lebih besar
        rb.AddForce(knockbackDir.normalized * knockbackForce, ForceMode2D.Impulse);

        // Log detail knockback untuk debugging
        Debug.Log($"Enemy Knockback: {knockbackDir}, Force: {knockbackForce}, Mass: {rb.mass}, Drag: {rb.drag}");
        Debug.DrawRay(transform.position, knockbackDir.normalized * 3f, Color.red, 1.0f);

        StartCoroutine(RecoverFromStagger());
    }

    private IEnumerator RecoverFromStagger()
    {
        yield return new WaitForSeconds(staggerDuration);
        isStaggered = false;
    }

    private void FlipSprite(float dirX)
    {
        if (dirX > 0.1f)
            transform.localScale = new Vector3(-5, 5, 1);
        else if (dirX < -0.1f)
            transform.localScale = new Vector3(5, 5, 1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (waypoints != null && waypoints.Length > 1)
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (waypoints[i] != null)
                {
                    Gizmos.DrawSphere(waypoints[i].position, 0.1f);
                    int nextIndex = (i + 1) % waypoints.Length;
                    if (waypoints[nextIndex] != null)
                        Gizmos.DrawLine(waypoints[i].position, waypoints[nextIndex].position);
                }
            }
        }
    }
}