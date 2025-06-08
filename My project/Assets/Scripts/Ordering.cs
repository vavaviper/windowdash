using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public GameObject car;

    public List<string> currentRequest = new List<string>();
    public bool dialogShowing = false;

    // Weight-based item categories: [0] = weight 1, [1] = weight 2, [2] = weight 3
    public static string[][] possibleOrders = new string[][]
    {
        new string[] {"Apple", "Orange", "Strawberry","Carrot","Pop","Soap","Soda","Toilet Paper","Tomato","Bread","Yogurt","Comb"},
        new string[] {"Cookie","Cucumber","Water Bottle","Ice Cream","Cake","Shampoo","Fish","Steak","Ham"},
        new string[] {"Cheese", "Juice Box", "Potatoes","Watermelon","Rice"}
    };

    public int minWeight = 10;
    public int maxWeight = 15;

    public int currentWeight;
    void Start()
    {
        GenerateRequest();
        CloseDialog();
    }

    void Update()
    {
        if (dialogShowing)
        {
            ShowDialog();

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogShowing)
            {
                CloseDialog();
                dialogShowing = false;
            }
            else
            {
                ShowDialog();
                dialogShowing = true;
            }
        }
    }

    public void GenerateRequest()
    {
        currentRequest.Clear();
        int currentWeight = 0;
        int attempt = 0;

        while (currentWeight < minWeight && attempt < 100)
        {
            int weightCategory = Random.Range(0, possibleOrders.Length); // 0 = weight 1, 1 = weight 2, 2 = weight 3
            string[] category = possibleOrders[weightCategory];
            if (category.Length == 0) continue;

            string selectedItem = category[Random.Range(0, category.Length)];
            int itemWeight = weightCategory + 1;

            if (currentWeight + itemWeight <= maxWeight)
            {
                currentRequest.Add(selectedItem);
                currentWeight += itemWeight;
            }

            attempt++;
        }
        int orderWeight = currentWeight;
        Debug.Log("Generated Order (Total Weight: " + currentWeight + "):");
        foreach (var item in currentRequest)
        {
            Debug.Log(item);
        }
    }

    public bool validInventory(List<string> curOrder)
    {
        List<string> itemNames = Inventory.instance.GetItemNames();
        foreach (string itemName in itemNames)
        {
            if (!curOrder.Contains(itemName))
            {
                return false;
            }
        }
        return true;
    }

    public void SubmitOrder()
    {
        if (validInventory(currentRequest))
        {
            List<string> itemNames = Inventory.instance.GetItemNames();
            foreach (string itemName in itemNames)
            {
                currentRequest.Remove(itemName);
            }
            Inventory.instance.ClearInventory();

            if (currentRequest.Count == 0)
            {
                Debug.Log("Order Completed!");
                car.GetComponent<Car>().SpeedChange();
            }
        }
        else
        {
            Debug.Log("Invalid item in the inventory, you must discard at the garbage bin");
        }
    }

    void ShowDialog()
    {
        if (dialogBox != null && dialogText != null)
        {
            dialogBox.SetActive(true);
            string result = "DELIVERY ORDER\n-------------------\n";
            foreach (var item in currentRequest)
            {
                result += $"• {item}\n";
            }
            dialogText.text = result;
            dialogShowing = true;
        }
    }

    void CloseDialog()
    {
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
            dialogShowing = false;
        }
    }
}
