using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player;

    private bool canSeePlayer = false;

    private void Update()
    {
        if (player == null) return;

        // Hitung jarak
        float distance = Vector2.Distance(transform.position, player.position);

        // Check Player Dalam Radius
        if (distance <= moveSpeed)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRadius, playerLayer);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                canSeePlayer = true;
            } else
            {
                canSeePlayer = false;
            }
        } else
        {
            canSeePlayer= false;
        }
        // Kejar Player
        if (canSeePlayer)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = canSeePlayer ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
