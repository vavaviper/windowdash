using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    [Header("Settings")]
    public string itemName;
    public float highlightIntensity = 0.1f;
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        itemCollider = GetComponent<BoxCollider2D>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        playerCollider = player.GetComponent<CircleCollider2D>();
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
        if (shouldHighlight)
        {
            Color gold = new Color(1f, 0.843f, 0f, 1f);
            spriteRenderer.color = new Color(
                gold.r * highlightIntensity,
                gold.g * highlightIntensity,
                gold.b * highlightIntensity,
                gold.a
            );
        }
        else
        {
            spriteRenderer.color = originalColor;
        }
    }
}