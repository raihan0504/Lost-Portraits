using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItem = 4;
    public InventorySlot[] InventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    private void Start()
    {
        ChangeSelectSlot(16);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            ChangeSelectSlot(16);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            ChangeSelectSlot(17);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){
            ChangeSelectSlot(18);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ChangeSelectSlot(19);
        }
    }

    void ChangeSelectSlot(int newValue)
    {
        if(selectedSlot >= 0)
        {
            InventorySlots[selectedSlot].Deselect();

        }

        InventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        // Check jika ada slot yang sama item dengan count lebih rendah dari maksimum
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && 
                itemInSlot.item == item &&
                itemInSlot.count < 4)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // Cari tempat invntory yang kosong
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item,slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = InventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if(itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                } else
                {
                    itemInSlot.RefreshCount();
                }
            }

            return item;
        }
        return null;
    }
}
