using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    private List<string> currentRequest = new List<string>();

    private string[][] possibleOrders = new string[][]
    {
        new string[] { "Apple", "Banana", "Orange", "Strawberry" },
        new string[] { "Rice", "Bread", "Cookies", "Cake" },
        new string[] { "Soda", "Pop", "Water", "Anti-Freeze" },
        new string[] { "Cheese", "Yogurt", "Ice Cream", "Milk" },
        new string[] { "T-bone steak", "Ham", "Dozen of eggs", "Fish" },
        new string[] { "Toilet paper", "Soap", "Comb", "Deodorant" },
        new string[] { "Prongles", "Dorders", "Chocolate", "Granola Bars" },
        new string[] { "Tomato", "Cucumber", "Carrot", "Potato" }
    };

    void Start()
    {
        GenerateRequest();
        ShowDialog();
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
        }
    }
}
