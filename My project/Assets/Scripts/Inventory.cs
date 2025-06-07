using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public Image[] inventorySlots; // Assign 3 UI Image slots in Inspector

    private int maxCapacity = 3;
    private int currentWeight = 0;

    private List<Item> items = new List<Item>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool TryAddItem(Item item)
    {
        if (currentWeight + item.weight > maxCapacity)
        {
            Debug.Log("Not enough space to add " + item.itemName);
            return false;
        }

        items.Add(item);
        currentWeight += item.weight;
        UpdateUI();
        return true;
    }

    void UpdateUI()
    {
        // Clear all slots
        foreach (var slot in inventorySlots)
        {
            slot.sprite = null;
            slot.enabled = false;
        }

        int slotIndex = 0;

        foreach (Item item in items)
        {
            for (int i = 0; i < item.weight; i++)
            {
                if (slotIndex >= inventorySlots.Length) return;

                inventorySlots[slotIndex].sprite = item.icon;
                inventorySlots[slotIndex].enabled = true;
                slotIndex++;
            }
        }
    }

    public void ClearInventory() // Optional, for testing
    {
        items.Clear();
        currentWeight = 0;
        UpdateUI();
    }
}
