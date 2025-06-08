using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeLimit = 60f; // seconds
    public TMP_Text timerText; // Or TMP_Text if using TextMeshPro
    public GameObject goodJobPanel;
    public GameObject failPanel;
    public Order orderScript;
    public StartScreenManager startScreen;

    private float currentTime;
    private bool levelEnded = false;

    // Simulated goal success (replace with your real condition)
    public bool playerSucceeded = false;

    void Start()
    {
        currentTime = timeLimit;
        goodJobPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    void Update()
    {
        if (levelEnded) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0);
        UpdateTimerUI();

        if (currentTime <= 0f)
        {
            checkScore(); // <-- Call this ONLY when time runs out
            EndLevel();
        }

        // Optional: Debug test for success (press L to simulate win)
        if (Input.GetKeyDown(KeyCode.L))
        {
            playerSucceeded = true;
        }
    }


    void checkScore()
    {
        int score = orderScript.score;
        int goal = startScreen.pointsGoal;
        if (score >= goal)
        {
            playerSucceeded = true;
        }
        else
        {
            playerSucceeded = false;
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void EndLevel()
    {
        levelEnded = true;

        // Disable player movement
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            player.canMove = false;
        }

        if (playerSucceeded)
        {
            goodJobPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }
    }


    IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public float timeUsed()
    {
        return timeLimit - currentTime;
    }
}