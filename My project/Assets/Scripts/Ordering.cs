using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform spawnPoint;
    public float carLifetime = 5f;

    private GameObject currentCar;
    private CircleCollider2D playerCollider;
    private BoxCollider2D carCollider;

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
    public Image dialogBackground;
    public string[] dialogLines;
    public float textSpeed = 0.05f;
    private string[] order;
    private GameObject player;
    private bool dialogShown = false;

    private bool dialogVisible = false;

    void Awake()
    {   
        SpawnCar();
        // Make dialog transparent at start
        SetDialogAlpha(0f);
        dialogBox.SetActive(false);
        carCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (playerCollider != null && carCollider != null)
        {
            if (playerCollider.IsTouching(carCollider))
            {
                if (!dialogShown)
                {
                    ShowDialog(order);
                    dialogShown = true;
                    Debug.Log("Player touched car - dialog shown.");
                }
            }
            else
            {
                if (dialogShown)
                {
                    HideDialog();
                    dialogShown = false;
                    Debug.Log("Player moved away - dialog hidden.");
                }
            }
        }

        if (dialogVisible && Input.GetKeyDown(KeyCode.S))
        {
            HideDialog();
            dialogShown = false;
        }
    }

    void SpawnCar()
    {
        currentCar = Instantiate(carPrefab, spawnPoint.position, Quaternion.identity);
        carCollider = currentCar.GetComponent<BoxCollider2D>();
        GenerateRequest();
        dialogShown = false;
    }

    void GenerateRequest()
    {
        currentRequest.Clear();

        int category1 = Random.Range(0, possibleOrders.Length);
        int category2 = Random.Range(0, possibleOrders.Length);
        int category3 = Random.Range(0, possibleOrders.Length);

        string item1 = possibleOrders[category1][Random.Range(0, 4)];
        string item2 = possibleOrders[category2][Random.Range(0, 4)];
        string item3 = possibleOrders[category3][Random.Range(0, 4)];

        currentRequest.Add(item1);
        currentRequest.Add(item2);
        currentRequest.Add(item3);

        order = new string[] {
            "NEW DELIVERY REQUEST",
            "-------------------",
            $"• {item1}",
            $"• {item2}", 
            $"• {item3}",
            "-------------------",
            "Collect these items!"
        };
    }

    public void ShowDialog(string[] lines)
    {
        string fullText = string.Join("\n", lines);
        dialogText.text = fullText;
        dialogBox.SetActive(true);
        SetDialogAlpha(1f);
        dialogVisible = true;
    }

    public void HideDialog()
    {
        SetDialogAlpha(0f);
        dialogBox.SetActive(false);
        dialogVisible = false;
    }

    void SetDialogAlpha(float alpha)
    {
        if (dialogBackground != null)
        {
            Color tempColor = dialogBackground.color;
            tempColor.a = alpha;
            dialogBackground.color = tempColor;
        }
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
