using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    public void ShowScreen()
    {
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
