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

public class WeaponUI : MonoBehaviour {

    [SerializeField] private PlayerCharacterController playerCharacterController;

    private Animator weaponAnimator;

    private void Awake() {
        weaponAnimator = GetComponent<Animator>();
    }

    private void Start() {
        playerCharacterController.OnStartMoving += PlayerCharacterController_OnStartMoving;
        playerCharacterController.OnStopMoving += PlayerCharacterController_OnStopMoving;
        playerCharacterController.OnShoot += PlayerCharacterController_OnShoot;
    }

    private void PlayerCharacterController_OnShoot(object sender, System.EventArgs e) {
        weaponAnimator.SetTrigger("shoot");
    }

    private void PlayerCharacterController_OnStopMoving(object sender, System.EventArgs e) {
        weaponAnimator.SetBool("isWalking", false);
    }

    private void PlayerCharacterController_OnStartMoving(object sender, System.EventArgs e) {
        weaponAnimator.SetBool("isWalking", true);
    }

}
