using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public GameObject car;
    public GameTimer gameTimer;
    public TMP_Text scoring;
    public bool orderTaken = false;
    public int score { get; private set; }

    public List<string> currentRequest = new List<string>();
    public bool dialogShowing = false;

    // Weight-based item categories: [0] = weight 1, [1] = weight 2, [2] = weight 3
    public static string[][] possibleOrders = new string[][]
    {
        new string[] {"Apple", "Orange", "Strawberry","Carrot","Pop","Soap","Soda","Toilet Paper","Tomato","Bread","Yogurt","Comb","Prongle","Dorders","Chocolate Bar"},
        new string[] {"Cookie","Cucumber","Water Bottle","Ice Cream","Cake","Shampoo","Fish","Steak","Ham","Milk","Granola Bars"},
        new string[] {"Cheese", "Juice Box", "Potatoes","Watermelon","Rice"}
    };

    public int minWeight = 1;
    public int maxWeight = 3;
    public int extraScoreWeight;
    public int orderWeight;
    public int currentWeight;
    void Start()
    {
        score = 0;
        GenerateRequest();
        CloseDialog();
        extraScoreWeight = (int)Mathf.Ceil((minWeight + maxWeight) / 2);

        if (dialogBox == null)
        {
            dialogBox = GameObject.Find("Panel");
        }
        if (dialogText == null && dialogBox != null)
        {
            dialogText = dialogBox.GetComponent<TextMeshProUGUI>();
        }
        if (car == null)
        {
            car = GameObject.Find("Car");
        }
        if (scoring == null)
        {
            scoring = GameObject.Find("ScoreBoard").GetComponentInChildren<TMP_Text>();
        }
    }

    void Update()
    {
        if (dialogShowing)
        {
            ShowDialog();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!orderTaken)
            {
                Debug.Log("You must take the order at the car");
                // for some reason this message shows twice when pressing space
            }
            else if (dialogShowing)
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
        orderTaken = false;

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
        orderWeight = currentWeight;
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
                updateScore();
                car.GetComponent<Car>().SpeedChange();
            }
        }
        else
        {
            Debug.Log("Invalid item in the inventory, you must discard at the garbage bin");
        }
    }
    public void updateScore()
    {
        if (currentWeight >= extraScoreWeight)
        {
            score += (int)(currentWeight * 1.5);
        }
        else
        {
            score += orderWeight;
        }
        scoring.text = score.ToString();
        Debug.Log("new score: " + score.ToString());
    }

    public void ShowDialog()
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

    public void CloseDialog()
    {
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
            dialogShowing = false;
        }
    }
}
