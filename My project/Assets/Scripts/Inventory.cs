using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

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

   public bool validInventory(Order order)
    {
        foreach (Item item in items)
        {
            if (!order.currentRequest.Contains(item.itemName))
            {
                Debug.Log("Invalid item in the inventory, you must discard the inventory");
                return false;
            }
        }
        return true;
    }

    void submitOrder(Order order)
    {
        if (validInventory(order))
        {
            Destroy(order.car);
            
            ClearInventory();
        }
    }
    void UpdateUI()
    {
        // Clear all slots
        foreach (var slot in inventorySlots)
        {
            slot.sprite = null;
            slot.enabled = false;
            slot.color = new Color(1f, 1f, 1f, 1f); // reset to full opacity
        }

        int slotIndex = 0;

        foreach (Item item in items)
        {
            for (int i = 0; i < item.weight; i++)
            {
                if (slotIndex >= inventorySlots.Length) return;

                inventorySlots[slotIndex].sprite = item.icon;
                inventorySlots[slotIndex].enabled = true;

                // First icon = full opacity, others = 60%
                float alpha = (i == 0) ? 1f : 0.6f;
                inventorySlots[slotIndex].color = new Color(1f, 1f, 1f, alpha);

                slotIndex++;
            }
        }
    }

    public List<string> GetItemNames()
    {
        List<string> itemNames = new List<string>();
        foreach (Item item in items)
        {
            itemNames.Add(item.itemName);
        }
        return itemNames;
    }



    public void ClearInventory() // Optional, for testing
    {
        items.Clear();
        currentWeight = 0;
        UpdateUI();
    }
}
