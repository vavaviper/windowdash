using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject player;

    private CircleCollider2D playerCircleCollider;
    private Item collidedObject;

    void Start()
    {
        playerCircleCollider = player.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.J) && collidedObject != null)
        {
            bool added = Inventory.instance.TryAddItem(collidedObject);
            if (added)
            {
                Debug.Log("Picked up: " + collidedObject.itemName);
            }
            else
            {
                Debug.Log("Inventory full. Could not pick up: " + collidedObject.itemName);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            collidedObject = item;
            Debug.Log("In range of: " + item.itemName);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && item == collidedObject)
        {
            collidedObject = null;
            Debug.Log("Left range of: " + item.itemName);
        }
    }
}
