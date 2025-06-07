using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    public string itemName = "Item";
    public Sprite inventoryIcon;
    
    public Color highlightColor = Color.yellow;
    public float highlightIntensity = 1.5f;
    public float highlightRadius = 1.5f; // How close player needs to be
    
    private SpriteRenderer spriteRenderer;
    private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        CircleCollider2D trigger = gameObject.AddComponent<CircleCollider2D>();
        trigger.radius = highlightRadius;
        trigger.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Highlight(false);
        }
    }

    void Highlight(bool enable)
    {
        GetComponent<SpriteRenderer>().color = enable ? Color.yellow : Color.white;
    }

}
