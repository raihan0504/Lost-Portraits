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
    private DamageFlash damageFlash;
    private Enemy enemy;

    private void Start()
    {
        currentHealth = maxHealth;
        knockback = GetComponent<Knockback>();
        anim = GetComponent<Animator>();
        damageFlash = GetComponent<DamageFlash>();
        enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(int damage, Vector2 knockbackDir)
    {
        currentHealth -= damage;

        if (enemy != null)
        {
            // Hanya enemy yang mendapat efek flash
            if (damageFlash != null)
            {
                damageFlash.Flash();
            }

            enemy.Stagger(knockbackDir);
        }
        else if (knockback != null)
        {
            knockback.ApplyKnockback(knockbackDir);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (anim != null)
        {
            anim.SetTrigger("die");
        }

        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(DelayedDestroy(1f));
    }

    IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}