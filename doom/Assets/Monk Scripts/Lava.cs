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

public class Lava : MonoBehaviour {

    private float damageTimer;
    private Material material;
    private Vector2 textureOffset;

    private void Awake() {
        material = transform.Find("Quad").GetComponent<MeshRenderer>().material;
    }

    private void Update() {
        textureOffset += new Vector2(+1f, -.5f) * .2f * Time.deltaTime;
        material.SetTextureOffset("_BaseColorMap", textureOffset);
        material.SetTextureOffset("_EmissiveColorMap", textureOffset);
    }

    private void OnTriggerStay(Collider other) {
        damageTimer -= Time.deltaTime;
        if (damageTimer <= 0f) {
            damageTimer += .06f;

            PlayerCharacterController playerCharacterController = other.GetComponent<PlayerCharacterController>();
            if (playerCharacterController != null) {
                playerCharacterController.Damage(1);
            }
        }
    }

}
