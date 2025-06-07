using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject player;
    public GameObject itemPrefab;

    private CircleCollider2D playerCircleCollider;
    public Item collidedObject;

    void Start()
    {
        playerCircleCollider = player.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        // Movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;

        // J key to spawn item
        if (Input.GetKeyDown(KeyCode.J))
        {
            Vector2 spawnPosition = new Vector2(3f, 2f);
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Called automatically when touching a trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        Item thing = other.GetComponent<Item>();
        if (thing != null)
        {
            collidedObject = thing;
            Debug.Log("Touched item: " + thing.name);
        }
    }
}
