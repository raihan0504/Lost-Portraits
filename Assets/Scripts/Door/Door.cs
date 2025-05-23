using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform teleportTarget;

    [Header("Confiner Settings")]
    public Collider2D confinerForTargetRoom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportTarget.position;
            ConfinerManager.Instance.SetConfiner(confinerForTargetRoom);
        }
    }
}
