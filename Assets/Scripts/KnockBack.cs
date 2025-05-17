using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    [SerializeField] float knockbackForce = 15f; // Tingkatkan dari 5f ke 15f
    [SerializeField] float recoveryTime = 0.3f;  // Waktu pemulihan setelah knockback

    [Header("Components")]
    private Rigidbody2D rb;
    private bool isBeingKnocked = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Pastikan settings rigidbody optimal untuk knockback
        if (rb != null && rb.mass > 1f)
        {
            Debug.Log($"Mass adjustment on {gameObject.name} from {rb.mass} to 1.0");
            rb.mass = 1f;
        }
    }

    public void ApplyKnockback(Vector2 dir)
    {
        StopAllCoroutines(); // Pastikan tidak ada coroutine yang tumpang tindih

        Debug.Log($"ApplyKnockback called on {gameObject.name} with direction: {dir}, force: {knockbackForce}");

        // Reset velocity sebelum menerapkan knockback
        rb.velocity = Vector2.zero;

        // Terapkan gaya knockback
        rb.AddForce(dir.normalized * knockbackForce, ForceMode2D.Impulse);

        // Visual debug
        Debug.DrawRay(transform.position, dir.normalized * 3f, Color.blue, 1.0f);

        // Set flag sedang knockback
        isBeingKnocked = true;

        // Mulai pemulihan setelah waktu tertentu
        StartCoroutine(RecoverFromKnockback());
    }

    private IEnumerator RecoverFromKnockback()
    {
        yield return new WaitForSeconds(recoveryTime);
        isBeingKnocked = false;
    }

    // Gunakan ini jika perlu mengetahui apakah sedang knockback
    public bool IsBeingKnocked()
    {
        return isBeingKnocked;
    }
}