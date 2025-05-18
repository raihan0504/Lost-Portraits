using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public int damage = 25;
    public Transform attack;
    public float knockbackMultiplier = 1.5f; // Faktor tambahan untuk knockback

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == attack.gameObject)
            return;

        Health targetHealth = collision.GetComponent<Health>();
        if (targetHealth != null)
        {
            // Hitung arah knockback dari titik serangan ke target
            Vector2 knockbackdir = (collision.transform.position - attack.position).normalized;

            // Tambahkan sedikit "lift" pada knockback untuk efek yang lebih baik
            // Ini akan memberikan sedikit gerakan Y pada knockback horizontal
            // yang terlihat lebih menarik secara visual
            knockbackdir.y += 0.2f;
            knockbackdir = knockbackdir.normalized * knockbackMultiplier;

            // Log info untuk debugging
            Debug.Log($"Attack hit {collision.name} with knockback: {knockbackdir}");

            // Visualisasi arah knockback
            Debug.DrawRay(collision.transform.position, knockbackdir * 2f, Color.green, 1.0f);

            // Terapkan damage dengan knockback
            targetHealth.TakeDamage(damage, knockbackdir);
        }
    }
}