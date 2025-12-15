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
//using CodeMonkey.Utils;

public class RotateTowardsCamera : MonoBehaviour {

    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
    }

    private void Update() {
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.y = 0f;
        Vector3 position = transform.position;
        position.y = 0f;

        Vector3 dirToCamera = (cameraPosition - position).normalized;
        float angleToCamera = GetAngleFromVectorFloat(dirToCamera);
        transform.eulerAngles = new Vector3(0f, -angleToCamera + 90 + 180, 0f);
    }
    
    public static float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

}
