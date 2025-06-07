using UnityEngine;

public class GarbageBin : MonoBehaviour
{
    private bool playerIsNear = false;
    private Renderer binRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    void Start()
    {
        binRenderer = GetComponent<Renderer>();
        originalColor = binRenderer.material.color;
    }

    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.J))
        {
            ClearInventory();
        }
    }

    // Called by child trigger script
    public void PlayerEntered()
    {
        playerIsNear = true;
        Highlight(true);
        Debug.Log("In range of garbage bin");
    }

    public void PlayerExited()
    {
        playerIsNear = false;
        Highlight(false);
        Debug.Log("Left range of garbage bin");
    }

    void Highlight(bool highlight)
    {
        binRenderer.material.color = highlight ? highlightColor : originalColor;
    }

    void ClearInventory()
    {
        Debug.Log("Inventory Cleared!");
        // Your inventory clearing logic here
        Inventory.instance.ClearInventory();
    }
}