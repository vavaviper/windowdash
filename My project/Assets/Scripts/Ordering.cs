using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public GameObject carPrefab; // Assign your car sprite prefab here
    public Transform spawnPoint; // Where the car should appear
    public float carLifetime = 5f;

    private GameObject currentCar;

    private List<string> currentRequest = new List<string>();
    private List<string> playerCart = new List<string>();

    private string[][] possibleOrders = new string[][]
    {
        new string[] { "Apple", "Banana", "Orange", "Strawberry"},
        new string[] { "Rice", "Bread", "Cookies", "Cake"},
        new string[] { "Soda", "Pop", "Water", "Anti-Freeze"},
        new string[] { "Cheese", "Yogurt", "Ice Cream", "Milk"}
    };
    
    public GameObject dialogBox;
    public Text dialogText;
    public string[] dialogLines;
    private int currentLine = 0;
    public float textSpeed = 0.05f;

    private bool dialogVisible = false;

    public void ShowDialog(string[] lines)
    {
        string fullText = string.Join("\n", lines);
        dialogText.text = fullText;
        dialogBox.SetActive(true);
        dialogVisible = true;
    }

    void Update()
    {
        // Hide dialog when any key is pressed and dialog is visible
        if (dialogVisible && Input.anyKeyDown)
        {
            dialogBox.SetActive(false);
            dialogVisible = false;
        }
    }

    void Start()
    {
        SpawnCar();
        dialogBox.SetActive(false);
    }
    void SpawnCar()
    {
        currentCar = Instantiate(carPrefab, spawnPoint.position, Quaternion.identity);
        GenerateRequest();
    }

    void GenerateRequest()
    {
        currentRequest.Clear();

        //Select Categories
        int category1 = Random.Range(0, possibleOrders.Length);
        int category2 = Random.Range(0, possibleOrders.Length);
        int category3 = Random.Range(0, possibleOrders.Length);
        
        // Select items
        string item1 = possibleOrders[category1][Random.Range(0, 4)];
        string item2 = possibleOrders[category2][Random.Range(0, 4)];
        string item3 = possibleOrders[category3][Random.Range(0, 4)];
        
        currentRequest.Add(item1);
        currentRequest.Add(item2);
        currentRequest.Add(item3);
        
        ShowDialog(new string[] {
            "NEW DELIVERY REQUEST",
            "-------------------",
            $"• {item1}",
            $"• {item2}", 
            $"• {item3}",
            "-------------------",
            "Collect these items!"
        });
    }

    public void SubmitCart(List<string> cart)
    {
        playerCart = new List<string>(cart);

        if (IsOrderCorrect())
        {
            Debug.Log("Order correct!");
            StartCoroutine(CompleteOrder());
        }
        else
        {
            Debug.Log("Order incorrect.");
        }
    }

    bool IsOrderCorrect()
    {
        if (playerCart.Count != currentRequest.Count)
            return false;

        List<string> tempRequest = new List<string>(currentRequest);

        foreach (var item in playerCart)
        {
            if (!tempRequest.Remove(item))
                return false;
        }

        return true;
    }

    IEnumerator CompleteOrder()
    {
        Destroy(currentCar);
        yield return new WaitForSeconds(1f);
        SpawnCar();
    }
}
