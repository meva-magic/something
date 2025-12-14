using UnityEngine;

namespace Common.Scripts
{
  public class ThirdPersonController : MonoBehaviour
  {
    #region Inspector

    [SerializeField]
    private float maxSpeed = 5f;

    [SerializeField]
    [Range(0.1f, 2f)]
    private float smoothTime = 0.2f; // Lower = faster acceleration

    [Header("Relations")]
    [SerializeField]
    private Rigidbody physicsBody = null;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    #endregion


    #region Fields

    private Vector3 _targetVelocity;
    private Vector3 _currentVelocity;
    private Vector3 _velocitySmoothing;

    #endregion


    #region MonoBehaviour

    private void Update()
    {
      // Get input
      float horizontal = 0;
      float vertical = 0;

      if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        vertical = 1;
      else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        vertical = -1;

      if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
      {
        horizontal = 1;
        spriteRenderer.flipX = false;
      }
      else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
      {
        horizontal = -1;
        spriteRenderer.flipX = true;
      }

      // Calculate target velocity based on input
      Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
      _targetVelocity = inputDirection * maxSpeed;
    }

    private void FixedUpdate()
    {
      // Smoothly accelerate/decelerate using SmoothDamp (exponential smoothing)
      _currentVelocity = Vector3.SmoothDamp(
        _currentVelocity, 
        _targetVelocity, 
        ref _velocitySmoothing, 
        smoothTime
      );
      
      // Apply velocity to rigidbody
      physicsBody.velocity = new Vector3(_currentVelocity.x, physicsBody.velocity.y, _currentVelocity.z);
    }

    #endregion
  }
}
