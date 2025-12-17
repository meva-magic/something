using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int Score;

    private void Awake()
    {
        instance = this;
    }

    public void SetScore()
    {
        Score = 100;

        Leaderboard.instance.SetEntry(PlayerPrefs.GetString("Username"), Score);
    }
}
