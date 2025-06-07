using UnityEngine;

public class GarbageBinTrigger : MonoBehaviour
{
    private GarbageBin garbageBin;

    void Start()
    {
        // Find the parent GarbageBin script
        garbageBin = GetComponentInParent<GarbageBin>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            garbageBin.PlayerEntered();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            garbageBin.PlayerExited();
        }
    }
}