using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    public int kills = 0;
    public int currentScore = 0;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            ResetScores();
        }
    }

    public void ResetScores()
    {
        currentScore = 0;
        kills = 0;
    }

    public void AddKill(int amount)
    {
        kills++;
        currentScore += amount;
    }
}
