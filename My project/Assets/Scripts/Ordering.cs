using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI dialogText;
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
                if (Input.GetKeyDown(KeyCode.J) && !dialogVisible)
                {
                    ShowOrderWithCheckmarks();
                    dialogShown = true;
                    Debug.Log("Order shown on J press.");
                }

                if (Input.GetKeyDown(KeyCode.K)) // maybe use K for submitting items
                {
                    SubmitCart(GetPlayerCart()); // You’d have to implement this function
                    Debug.Log("Submitted cart.");
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
    List<string> GetPlayerCart()
    {
        return Inventory.instance.GetItemNames();
    }



    void SpawnCar()
    {
        currentCar = Instantiate(carPrefab, spawnPoint.position, Quaternion.identity);
        carCollider = currentCar.GetComponent<BoxCollider2D>();
        GenerateRequest();
        dialogShown = false;
    }


    public void ShowOrderWithCheckmarks()
    {
        string[] display = new string[currentRequest.Count + 3];
        display[0] = "DELIVERY TICKET";
        display[1] = "-------------------";
        for (int i = 0; i < currentRequest.Count; i++)
        {
            string item = currentRequest[i];
            bool found = playerCart.Contains(item);
            display[i + 2] = found ? $"✅ {item}" : $"❌ {item}";
        }

        dialogText.text = string.Join("\n", display);
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

        SpawnItems(item1, item2, item3);
    }

    public GameObject itemPrefab;

    void SpawnItems(string item1, string item2, string item3)
    {
        // USE THIS TO SPAWN THE ICONS ON TICKETS
        Vector2 basePos = new Vector2(0, 0); // Choose spawn origin
        InstantiateItem(item1, basePos + new Vector2(1, 1));
        InstantiateItem(item2, basePos + new Vector2(-1, 1));
        InstantiateItem(item3, basePos + new Vector2(0, -1));
    }

    void InstantiateItem(string itemName, Vector2 position)
    {
        GameObject item = Instantiate(itemPrefab, position, Quaternion.identity);
        item.name = itemName;
        item.GetComponent<Item>().itemName = itemName; // Assuming an Item script with string itemName
    }


}
