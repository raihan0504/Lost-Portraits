using UnityEngine;

public class RoomTeleport : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform teleportTarget;

    [Header("Confiner Settings")]
    public Collider2D confinerForTargetRoom;

    [Header("Key Requirement")]
    public Item requiredKey;

    private bool isPlayerNearby = false;
    private Transform player;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.C))
        {
            Item selectedItem = InventoryManager.Instance.GetSelectedItem(false); // false = jangan konsumsi dulu

            if (selectedItem != null &&
                selectedItem.itemType == ItemType.Key &&
                selectedItem.actionType == ActionType.Unlock &&
                selectedItem == requiredKey)
            {
                // Gunakan kunci dan teleport
                InventoryManager.Instance.GetSelectedItem(true); // true = konsumsi item
                Teleport();
                Debug.Log("Teleportasi berhasil dengan kunci yang sesuai: " + selectedItem.name);
            }
            else
            {
                Debug.Log("Teleport gagal. Dibutuhkan kunci khusus: " + requiredKey.name);
            }
        }
    }

    void Teleport()
    {
        if (player == null) return;

        player.position = teleportTarget.position;
        ConfinerManager.Instance.SetConfiner(confinerForTargetRoom);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.transform;
            Debug.Log("Dekat pintu teleport. Tekan C untuk menggunakan kunci.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
            Debug.Log("Menjauh dari pintu teleport.");
        }
    }
}
