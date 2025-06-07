using UnityEngine;

public class Car : MonoBehaviour
{   
    public float speed;
    private Vector3 startPosition;
    private float screenRightEdge;
    public bool orderValid = true;

    void Start()
    {
        startPosition = transform.position;
        screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1f; 
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.position.x > screenRightEdge)
        {
            transform.position = startPosition;
            orderValid = false;
        }
    }
}
