using UnityEngine;
using TMPro;

public class StartScreenManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject startScreenPanel;
    public TextMeshProUGUI startScreenText;

    [Header("Settings")]
    public int pointsGoal = 10;

    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f; // Pause the game
        gameStarted = false;

        startScreenPanel.SetActive(true);
        startScreenText.text = $"{pointsGoal}";
    }

    void Update()
    {
        if (!gameStarted && Input.anyKeyDown)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        startScreenPanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}
