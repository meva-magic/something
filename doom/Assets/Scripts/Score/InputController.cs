using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InputController : MonoBehaviour
{
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerScore;

    [SerializeField] private string[] randomNames = {"Rat", "Bugs", "Bee"};

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Username"))
        {
            PlayerPrefs.SetString("Username", randomNames[Random.Range(0, randomNames.Length)]);
        }

        playerName.text = PlayerPrefs.GetString("Username");

        submitButton.onClick.AddListener(() => SaveName());
    }

    private void SaveName()
    {
        if (inputField.text != "" && inputField.text.Length <+ 17)
        {
            PlayerPrefs.SetString("Username", inputField.text);

            playerName.text = PlayerPrefs.GetString("Username");
            inputField.text = "";
        }

        else
        {
            inputField.text = "Error :(";
        }
    }
}
