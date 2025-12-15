using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Added to Key
 * */
public class DoorKey : MonoBehaviour 
{
    
    [Header("Door Key")]
    [Tooltip("The Key Scriptable Object")]
    public Key key;

    public void DestroySelf() 
    {
        // Destroy this Key
        Destroy(gameObject);
    }

}
