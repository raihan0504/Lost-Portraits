using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 7f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void KnockbackFrom(Vector2 sourcePosition)
    {
        Vector2 direction = ((Vector2) transform.position - sourcePosition).normalized;
        rb.AddForce(direction * knockbackForce);
    }
}
