using UnityEngine;
using UnityEngine.UIElements;

public class Car : MonoBehaviour
{
    public float normalspeed = 0.3f;
    public float driveAwaySpeed = 20f;
    private Vector3 startPosition;
    private float screenRightEdge;
    private bool playerIsNear = false;
    public Order orderScript;

    private Renderer carRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow;
    private float speed;

    void Start()
    {
        startPosition = transform.position;
        speed = normalspeed;
        carRenderer = GetComponent<Renderer>();
        screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1f;
        originalColor = carRenderer.material.color;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.position.x > screenRightEdge)
        {
            transform.position = startPosition;
            speed = normalspeed;
            orderScript.GenerateRequest();
            orderScript.CloseDialog();
        }
        if (playerIsNear)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                orderScript.SubmitOrder();
            }
            if (Input.GetKeyDown(KeyCode.Space) && (!orderScript.orderTaken))
            {
                orderScript.orderTaken = true;
                orderScript.ShowDialog();
            }
        }
    }

    void Highlight(bool highlight)
    {
        carRenderer.material.color = highlight ? highlightColor : originalColor;
    }
    // Called by child trigger script
    public void PlayerEntered()
    {
        playerIsNear = true;
        Highlight(true);
        Debug.Log("In range of car");
    }

    public void PlayerExited()
    {
        playerIsNear = false;
        Highlight(false);
        Debug.Log("Left range of car");
    }

    public void SpeedChange()
    {
        speed = driveAwaySpeed;
    }
}
