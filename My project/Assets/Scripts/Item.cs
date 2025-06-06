using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    public string itemName = "Item";
    public Sprite inventoryIcon;
    
    public Color highlightColor = Color.yellow;
    public float highlightIntensity = 1.5f;
    public float highlightRadius = 1.5f; // How close player needs to be
    
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isHighlighted = false;
    private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        CircleCollider2D proximityCollider = gameObject.AddComponent<CircleCollider2D>();
        proximityCollider.radius = highlightRadius;
        proximityCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HighlightItem(true);
        }
    }
    
    void Pickup(PlayerInventory playerInventory)
    {
        
    }

}
