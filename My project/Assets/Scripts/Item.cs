using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    public string itemName = "Item";
    public Sprite inventoryIcon;
    
    public float highlightIntensity = 1.5f;
    public float highlightRadius = 1.5f;
    
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float weight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        
        CircleCollider2D trigger = gameObject.AddComponent<CircleCollider2D>();
        trigger.radius = highlightRadius;
        trigger.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Highlight(true);
        }
    }

    void Highlight(bool enable)
    {
        spriteRenderer.color = enable ? 
            Color.yellow * highlightIntensity : 
            originalColor;
    }
}
