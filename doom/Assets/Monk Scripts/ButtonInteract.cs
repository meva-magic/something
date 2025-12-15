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

public class ButtonInteract : MonoBehaviour, IInteractable {

    [SerializeField] private Sprite buttonOffSprite;
    [SerializeField] private Sprite buttonOnSprite;

    [SerializeField] private DoorLock doorLock;

    private bool isButtonOn;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        isButtonOn = false;
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    public void Interact() {
        isButtonOn = !isButtonOn;
        spriteRenderer.sprite = isButtonOn ? buttonOnSprite : buttonOffSprite;
        if (isButtonOn) doorLock.OpenDoor();
        else doorLock.CloseDoor();
    }

}
