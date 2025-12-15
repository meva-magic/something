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

public class Elevator : MonoBehaviour, IInteractable {

    private Animator animator;
    private bool elevatorMoveUp;

    private void Awake() {
        animator = GetComponent<Animator>();
        elevatorMoveUp = false;
    }

    public void Interact() {
        elevatorMoveUp = !elevatorMoveUp;
        animator.SetBool("moveUp", elevatorMoveUp);
    }

    public void SetElevatorMoveUp(bool elevatorMoveUp) {
        this.elevatorMoveUp = elevatorMoveUp;
        animator.SetBool("moveUp", elevatorMoveUp);
    }

    private void OnTriggerEnter(Collider collider) {
        PlayerCharacterController playerCharacterController = collider.GetComponent<PlayerCharacterController>();
        if (playerCharacterController != null) {
            playerCharacterController.transform.SetParent(transform);
            //collider.GetComponent<CharacterController>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider collider) {
        PlayerCharacterController playerCharacterController = collider.GetComponent<PlayerCharacterController>();
        if (playerCharacterController != null) {
            playerCharacterController.transform.SetParent(null);
            //collider.GetComponent<CharacterController>().enabled = true;
        }
    }

}
