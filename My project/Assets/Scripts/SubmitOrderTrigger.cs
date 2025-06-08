using UnityEngine;

public class SubmitOrderTrigger : MonoBehaviour
{
    private Car car;
    
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
}
