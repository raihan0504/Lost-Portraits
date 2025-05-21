using UnityEngine;

public class NoteInteraction : MonoBehaviour
{
    [TextArea]
    public string noteContent; // isi teks yang akan ditampilkan saat dibaca

    private bool isPlayerNearby = false;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            NoteUIManager.Instance.ShowNote(noteContent);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
