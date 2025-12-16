using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Range(0.001f, 0.01f)]
    [SerializeField] private float amount = 0.002f;

    [Range(10f, 100f)]
    [SerializeField] private float smooth = 10f;

    [Range(1f, 30f)]
    [SerializeField] private float frequency = 10f;

    private float inputMagnitude;
    private Vector3 startPos;
    private float bobTimer;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        CheckForMovement();
    }

    private void CheckForMovement()
    {
        inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if (inputMagnitude > 0)
        {
            bobTimer += Time.deltaTime * frequency;
            Vector3 newPosition = startPos + CalculateHeadBob(bobTimer);
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, smooth * Time.deltaTime);
        }
        else
        {
            bobTimer = 0;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, smooth * Time.deltaTime);
        }
    }

    private Vector3 CalculateHeadBob(float timer)
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(timer) * amount * 1.4f;
        pos.x += Mathf.Cos(timer * 0.5f) * amount * 1.6f;
        return pos;
    }
}
