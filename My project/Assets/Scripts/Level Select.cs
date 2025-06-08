using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    public GameObject[] levelButtons;

    void Start()
    {
        void Start()
        {
            PlayerPrefs.DeleteKey("Level1Completed"); // Level 2 is unlocked when Level 1 is completed
            PlayerPrefs.Save();
            Debug.Log("Level 2 has been locked again.");

        }

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool unlocked = (i == 0) || PlayerPrefs.GetInt("Level" + i + "Completed", 0) == 1;

            Button btn = levelButtons[i].GetComponent<Button>();
            if (btn != null)
            {
                btn.interactable = unlocked;
            }

            Image btnImage = levelButtons[i].GetComponent<Image>();
            if (btnImage != null)
            {
                btnImage.color = unlocked ? Color.white : new Color(0.6f, 0.6f, 0.6f); // Greyed out
            }
        }
    }


    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}