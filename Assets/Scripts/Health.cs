using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Component")]
    private Knockback knockback;
    private Animator anim;

    private void Start()
    {
        currentHealth = maxHealth;
        knockback = GetComponent<Knockback>();
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage, Vector2 knockbackDir)
    {
        currentHealth -= damage;

        // Pilih SALAH SATU metode knockback, jangan keduanya
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            // Gunakan stagger untuk enemy
            enemy.Stagger(knockbackDir);
            Debug.Log("Enemy knockback with force 20f");
        }
        else if (knockback != null)
        {
            // Gunakan knockback component untuk non-enemy
            knockback.ApplyKnockback(knockbackDir);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetTrigger("die");
        Debug.Log(gameObject.name + " mati");
        // Nonaktifkan kontrol agar player tidak bisa gerak
        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // Hancurkan objek setelah animasi selesai (misal 1 detik)
        StartCoroutine(DelayedDestroy(1f));
    }

    IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}