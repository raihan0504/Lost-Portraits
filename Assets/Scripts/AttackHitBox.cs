using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            KnockBack knockback = collision.GetComponent<KnockBack>();
            if (knockback != null)
            {
                knockback.KnockbackFrom(player.transform.position);
            }
        }
    }
}
