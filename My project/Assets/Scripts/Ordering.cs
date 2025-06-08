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

    public static string[][] possibleOrders = new string[][]
    {
        new string[] {"Apple", "Orange", "Strawberry","Carrot","Pop","Soap","Soda","Tomato","Yogurt"},
        new string[] {"Cookie","Cucumber","Water Bottle","Ice Cream"},
        new string[] {"Cheese", "Juice Box", "Potatoes","Watermelon"}
    };

    void Start()
    {
        GenerateRequest();
        CloseDialog();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogShowing)
            {
                CloseDialog();
            }
            else
            {
                ShowDialog();
            }
        }
    }

    void GenerateRequest()
    {
        currentRequest.Clear();

        for (int i = 0; i < 3; i++)
        {
            int cat = Random.Range(0, possibleOrders.Length);
            int item = Random.Range(0, possibleOrders[cat].Length);
            currentRequest.Add(possibleOrders[cat][item]);
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
                GenerateRequest();
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
