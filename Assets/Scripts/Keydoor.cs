using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyDoor : MonoBehaviour
{
    public string sceneToLoad;
    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Item selectedItem = InventoryManager.Instance.GetSelectedItem(false);
            if (selectedItem != null && selectedItem.itemType == ItemType.Key)
            {
                Debug.Log("Key item detected. Loading scene: " + sceneToLoad);
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.Log("You need a key to open this door!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
