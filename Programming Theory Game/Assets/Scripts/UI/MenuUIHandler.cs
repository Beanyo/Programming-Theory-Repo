using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public string highPlayerName;
    public string currentPlayerName;
    public GameObject inputFieldObj;
    public Text ScoreText;
    public int highScore;

    // Start is called before the first frame update
    void Start()
    {
        PullSavedScore();
        UpdateBestScore();
    }
    public void PullSavedScore()
    {
        MenuManager.Instance.LoadScore();
        highScore = MenuManager.Instance.highScore;
        highPlayerName = MenuManager.Instance.highPlayerName;
    }
    public void UpdateBestScore()
    {
        ScoreText.text = "Best Score: " + highPlayerName + " : " + highScore;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GetPlayerName()
    {
        InputField inputField = inputFieldObj.GetComponent<InputField>();
        currentPlayerName = inputField.text;
        if (string.IsNullOrEmpty(currentPlayerName))
        {
            currentPlayerName = "Anon";
        }
        MenuManager.Instance.currentPlayerName = currentPlayerName;
    }
    public void ResetBest()
    {
        highPlayerName = "None";
        highScore = 0;
        UpdateBestScore();
        MenuManager.Instance.currentPlayerName = highPlayerName;
        MenuManager.Instance.highScore = highScore;
        MenuManager.Instance.SaveScore();
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
