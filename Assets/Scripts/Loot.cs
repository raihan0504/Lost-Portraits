using System.Collections;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [Header("Item Data")]
    [SerializeField] private Item item;

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float moveSpeed = 5f;

    private bool isCollected = false;

    private void Awake()
    {
        if (item != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = item.image;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected) return;

        if (collision.CompareTag("Player"))
        {
            bool canAdd = InventoryManager.Instance.AddItem(item);
            if (canAdd)
            {
                isCollected = true;
                StartCoroutine(MoveAndCollect(collision.transform));
            }
        }
    }

    private IEnumerator MoveAndCollect(Transform target)
    {
        circleCollider.enabled = false;

        while (Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
