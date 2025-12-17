using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;

    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private string publicKey = "870c9e6d8ae9b8436af44236d0f05106706c8a8dc8c060c18b7f99ae8ad916d1";

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadEntries();
    }

    private void LoadEntries()
    {
        Leaderboards.MyLeaderboard.GetEntries(entries =>
        {
            foreach(TextMeshProUGUI name in names)
            {
                name.text = "";
            }

            foreach(TextMeshProUGUI score in scores)
            {
                score.text = "";
            }

            float length = Mathf.Min(names.Count, entries.Length);

            for (int i = 0; i < length; i++)
            {
                names[i].text = entries[i].Username;
                scores[i].text = entries[i].Score.ToString();
            }
        });
    }

    public void SetEntry(string username, int score)
    {
        Leaderboards.MyLeaderboard.UploadNewEntry(username, score, isSuccessful =>
        {
            if (isSuccessful)
            {
                LoadEntries();
            }
        });
    }
}
