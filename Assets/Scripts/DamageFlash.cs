using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public Material flashMaterial;
    public float flashDuration = 0.1f;

    private Material originalMaterial;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
    }

    public void Flash()
    {
        if (flashMaterial != null && spriteRenderer != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;
        flashMaterial.SetFloat("_FlashAmount", 1f);

        yield return new WaitForSeconds(flashDuration);

        flashMaterial.SetFloat("_FlashAmount", 0f);
        spriteRenderer.material = originalMaterial;
    }
}
