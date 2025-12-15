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

public class DoorSimple : MonoBehaviour, IInteractable 
{
    
    Animator m_Animator;
    private bool isOpen;

    void Awake() 
    {
        // Cache Animator Component
        m_Animator = GetComponent<Animator>();
        isOpen = false;
    }

    public void OpenDoor() 
    {
        // Play Open Door Animation
        m_Animator.SetTrigger("Open");
    }

    public void CloseDoor() {
        // Play Close Door Animation
        m_Animator.SetTrigger("Close");
    }

    public void Interact() {
        isOpen = !isOpen;
        if (isOpen) {
            OpenDoor();
        } else {
            CloseDoor();
        }
    }

}
