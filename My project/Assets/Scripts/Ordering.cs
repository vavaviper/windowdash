using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOrderSystem : MonoBehaviour
{
    public GameObject carPrefab; // Assign your car sprite prefab here
    public Transform spawnPoint; // Where the car should appear
    public float carLifetime = 5f;

    private GameObject currentCar;

    private List<string> currentRequest = new List<string>();
    private List<string> playerCart = new List<string>();

    private string[][] possibleOrders = new string[][]
    {
        new string[] { "Apple", "Banana" },
        new string[] { "Milk", "Bread" },
        new string[] { "Soda" },
        new string[] { "Cheese", "Ham", "Bread" }
    };

    void Start()
    {
        SpawnCar();
    }

    void SpawnCar()
    {
        currentCar = Instantiate(carPrefab, spawnPoint.position, Quaternion.identity);
        GenerateRequest();
    }

    void GenerateRequest()
    {
        currentRequest.Clear();
        string[] order = possibleOrders[Random.Range(0, possibleOrders.Length)];
        currentRequest.AddRange(order);
        Debug.Log("Car Request: " + string.Join(", ", currentRequest));
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
