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

public class WallInteractable : MonoBehaviour {

    [SerializeField] private GameObject wallGameObject;

    private void OnTriggerEnter(Collider collider) {
        PlayerCharacterController playerCharacterController = collider.GetComponent<PlayerCharacterController>();
        if (playerCharacterController != null) {
            wallGameObject.SetActive(false);
        }
    }

}
