using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI killsText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void ShowScreen()
    {
        killsText.text = ScoreController.instance.kills.ToString();
        scoreText.text = ScoreController.instance.currentScore.ToString();
        AudioManager.instance.Play("Defeat");
        gameOverScreen.SetActive(true);
    }

    public void HideScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Arena", LoadSceneMode.Single);
    }
}
