using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryUI;
    private bool isInventoryOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryUI.SetActive(isInventoryOpen);

            Time.timeScale = isInventoryOpen ? 0 : 1;
        }
    }
}
