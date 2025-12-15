using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour {

    public void DestroySelf() {
        Destroy(gameObject);
    }

}
