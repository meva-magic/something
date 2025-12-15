/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour {

    [SerializeField] private PlayerCharacterController playerCharacterController;

    private Text ammoText;
    private Text healthText;

    private void Awake() {
        ammoText = transform.Find("ammoText").GetComponent<Text>();
        healthText = transform.Find("healthText").GetComponent<Text>();
    }

    private void Start() {
        playerCharacterController.GetHealthSystem().OnHealthChanged += GameHUD_OnHealthChanged;
        playerCharacterController.OnAmmoCountChanged += PlayerCharacterController_OnAmmoCountChanged;

        Refresh();
    }

    private void GameHUD_OnHealthChanged(object sender, System.EventArgs e) {
        Refresh();
    }

    private void PlayerCharacterController_OnAmmoCountChanged(object sender, System.EventArgs e) {
        Refresh();
    }

    private void Refresh() {
        ammoText.text = playerCharacterController.GetAmmoCount().ToString();
        healthText.text = playerCharacterController.GetHealthSystem().GetHealth() + "%";
    }

}
