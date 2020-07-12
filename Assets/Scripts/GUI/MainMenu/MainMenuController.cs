using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartPlaying()
    {
        SceneManager.LoadScene("Arena", LoadSceneMode.Single);
    }
}
