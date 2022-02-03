using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    public Text ScoreText;
    public Text HighScoreText;
    public Text Health;
    public Text GameOverText;
    public int playerHealth;
    public int highScore;
    public int currentScore;
    private bool gameOver = false;
    private string bestPlayerName;
    private string currentPlayerName;

    // Start is called before the first frame update
    void Start()
    {
        SetBestText();
        currentPlayerName = MenuManager.Instance.currentPlayerName;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            UpdateText();
        }
        else
        {
            UpdateText();
            GameOverSelection();
        }
    }

    private void GameOverSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void UpdateText()
    {
        Health.text = "Health: " + playerHealth;
        ScoreText.text = "Current Score: " + currentPlayerName + ": " + currentScore;
    }

    private void SetBestText()
    {
        MenuManager.Instance.LoadScore();
        highScore = MenuManager.Instance.highScore;
        bestPlayerName = MenuManager.Instance.highPlayerName;
        HighScoreText.text = "Best Score: " + bestPlayerName + " : " + highScore;
    }

    public void GameOverGenerate()
    {
        gameOver = true;
        GameOverText.gameObject.SetActive(true);
        if(currentScore > highScore)
        {
            MenuManager.Instance.highScore = currentScore;
            MenuManager.Instance.currentPlayerName = currentPlayerName;
            MenuManager.Instance.SaveScore();
        }
    }



}
