using UnityEngine;

public class SubmitOrderTrigger : MonoBehaviour
{
    private Car car;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        car = GetComponentInParent<Car>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            car.PlayerEntered();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            car.PlayerExited();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
