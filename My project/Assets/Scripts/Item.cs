using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    [Header("Settings")]
    public string itemName;
    public float highlightIntensity = 1.5f;
    public int weight;
    public Sprite icon;
    
    [Header("References")]
    [SerializeField] private GameObject player; // Drag player in Inspector
    
    // Components
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private BoxCollider2D itemCollider;
    private CircleCollider2D playerCollider;

    void Awake()
    {
        // Get own components
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        itemCollider = GetComponent<BoxCollider2D>();
        
        // Verify player reference
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("No player found! Assign player in Inspector or tag a GameObject as 'Player'");
                return;
            }
        }
        
        // Get player components
        playerCollider = player.GetComponent<CircleCollider2D>();
        if (playerCollider == null)
        {
            Debug.LogError("Player needs a CircleCollider2D!");
        }
    }

    void Update()
    {
        if (playerCollider != null && itemCollider != null)
        {
            if (playerCollider.IsTouching(itemCollider))
            {
                Highlight(true);
            }
            else
            {
                Highlight(false);
            }
        }
    }

    void Highlight(bool shouldHighlight)
    {
        spriteRenderer.color = shouldHighlight ? 
            Color.yellow * highlightIntensity : 
            originalColor;
    }
}