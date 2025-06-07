using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public GameObject dashEffectPrefab;
    public float moveSpeed = 7f;
    public float dashSpeed = 14f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 0.5f;

    public GameObject player;

    private CircleCollider2D playerCircleCollider;
    private Item collidedObject;

    private bool isDashing = false;
    private float dashCooldownTimer = 0f;
    private Vector2 lastMoveDirection = Vector2.down; // Default direction

    public bool canMove = true;


    void Start()
    {
        playerCircleCollider = player.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (!canMove) return; // Stop everything if movement is disabled

        if (!isDashing)
        {
            HandleMovement();
            HandleInteraction();
            clearInventory();

            if (Input.GetKeyDown(KeyCode.K) && dashCooldownTimer <= 0f)
            {
                StartCoroutine(PerformDash());
            }
        }
    }


    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir != Vector2.zero)
        {
            lastMoveDirection = moveDir;
        }

        transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;
    }

    IEnumerator PerformDash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        // Spawn particle effect
        if (dashEffectPrefab != null)
        {
            GameObject effect = Instantiate(dashEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f); // Auto-destroy effect after it's finished
        }

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            transform.position += (Vector3)lastMoveDirection * dashSpeed * Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }

    void clearInventory()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Inventory.instance.ClearInventory();
        }
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
