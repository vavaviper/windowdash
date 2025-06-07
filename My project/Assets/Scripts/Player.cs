using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject player;
    public Item item;

    private CircleCollider2D playerCircleCollider;
    private BoxCollider2D itemBoxCollider;

    void Start()
    {
        playerCircleCollider = player.GetComponent<CircleCollider2D>();
        itemBoxCollider = item.GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;

        if (playerCircleCollider.IsTouching(itemBoxCollider) & Input.GetKeyDown(KeyCode.J))
        {
            Vector2 spawnPosition = new Vector2(3f, 2f);
            Instantiate(item, spawnPosition, Quaternion.identity);

        }



    }
}