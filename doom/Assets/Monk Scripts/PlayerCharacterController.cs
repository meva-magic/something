/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {

    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;
    public event EventHandler OnShoot;
    public event EventHandler OnAmmoCountChanged;

    private const float NORMAL_FOV = 60f;
    private const float HOOKSHOT_FOV = 100f;

    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private Transform debugHitPointTransform;

    private CharacterController characterController;
    private float cameraVerticalAngle;
    private float characterVelocityY;
    private Camera playerCamera;
    private Animator playerCameraAnimator;
    private CameraFov cameraFov;
    private State state;
    private bool isMoving;

    private Vector3 characterVelocityMomentum;
    private HealthSystem healthSystem;
    private int ammoCount;

    private enum State {
        Normal,
    }

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        playerCamera = transform.Find("Camera").GetComponent<Camera>();
        playerCameraAnimator = playerCamera.GetComponent<Animator>();
        cameraFov = playerCamera.GetComponent<CameraFov>();
        Cursor.lockState = CursorLockMode.Locked;
        state = State.Normal;

        isMoving = false;
        healthSystem = new HealthSystem(200);
        ammoCount = 10;

        OnStartMoving += PlayerCharacterController_OnStartMoving;
        OnStopMoving += PlayerCharacterController_OnStopMoving;
    }

    private void PlayerCharacterController_OnStopMoving(object sender, EventArgs e) {
        playerCameraAnimator.SetBool("isWalking", isMoving);
    }

    private void PlayerCharacterController_OnStartMoving(object sender, EventArgs e) {
        playerCameraAnimator.SetBool("isWalking", isMoving);
    }

    private void Update() {
        switch (state) {
        default:
        case State.Normal:
            HandleCharacterLook();
            HandleCharacterMovement();
            HandleShooting();
            HandleInteract();
            break;
        }
    }

    private void HandleCharacterLook() {
        float lookX = Input.GetAxisRaw("Mouse X");
        float lookY = 0f;// Input.GetAxisRaw("Mouse Y");

        // Rotate the transform with the input speed around its local Y axis
        transform.Rotate(new Vector3(0f, lookX * mouseSensitivity, 0f), Space.Self);

        // Add vertical inputs to the camera's vertical angle
        cameraVerticalAngle -= lookY * mouseSensitivity;

        // Limit the camera's vertical angle to min/max
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

        // Apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
    }

    private void HandleCharacterMovement() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        float moveSpeed = 30f;

        Vector3 lastPosition = transform.position;

        Vector3 characterVelocity = (transform.right * moveX * moveSpeed + transform.forward * moveZ * moveSpeed);

        if (characterController.isGrounded) {
            characterVelocityY = 0f;
            // Jump
            if (TestInputJump()) {
                float jumpSpeed = 30f;
                characterVelocityY = jumpSpeed;
            }
        }

        // Apply gravity to the velocity
        float gravityDownForce = -110f;
        characterVelocityY += gravityDownForce * Time.deltaTime;


        // Apply Y velocity to move vector
        characterVelocity.y = characterVelocityY;

        // Apply momentum
        characterVelocity += characterVelocityMomentum;

        // Move Character Controller
        characterController.Move(characterVelocity * Time.deltaTime);

        // Dampen momentum
        if (characterVelocityMomentum.magnitude > 0f) {
            float momentumDrag = 3f;
            characterVelocityMomentum -= characterVelocityMomentum * momentumDrag * Time.deltaTime;
            if (characterVelocityMomentum.magnitude < .0f) {
                characterVelocityMomentum = Vector3.zero;
            }
        }

        Vector3 newPosition = transform.position;

        if (newPosition != lastPosition) {
            // Moved
            if (!isMoving) {
                // Wasn't moving
                isMoving = true;
                OnStartMoving?.Invoke(this, EventArgs.Empty);
            }
        } else {
            // Didn't move
            if (isMoving) {
                // Was moving
                isMoving = false;
                OnStopMoving?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void HandleShooting() {
        if (Input.GetMouseButtonDown(0) && TryShootAmmo()) {
            Vector3 halfBoxSize = new Vector3(.7f, .75f, 20f);
            float playerHeightOffset = .8f;
            Collider[] colliderArray = Physics.OverlapBox(transform.position + transform.up * playerHeightOffset + transform.forward * halfBoxSize.z, halfBoxSize, transform.rotation);
            foreach (Collider collider in colliderArray) {
                ShootingTarget shootingTarget = collider.GetComponent<ShootingTarget>();
                if (shootingTarget != null) {
                    shootingTarget.Damage();
                }
            }
            OnShoot?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandleInteract() {
        if (Input.GetKeyDown(KeyCode.E)) {
            float interactRadius = 5f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRadius);
            foreach (Collider collider in colliderArray) {
                IInteractable interactable = collider.GetComponent<IInteractable>();
                if (interactable != null) {
                    // There's an Interatable object in range!
                    interactable.Interact();
                }
            }
        }
    }

    private void ResetGravityEffect() {
        characterVelocityY = 0f;
    }

    private bool TestInputJump() {
        return false;// Input.GetKeyDown(KeyCode.Space);
    }

    public void Damage(int damageAmount) {
        healthSystem.Damage(damageAmount);
    }

    public void Heal(int healAmount) {
        healthSystem.Heal(healAmount);
    }

    public HealthSystem GetHealthSystem() {
        return healthSystem;
    }

    public bool TryShootAmmo() {
        if (ammoCount > 0) {
            ammoCount--;
            OnAmmoCountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        } else {
            return false;
        }
    }

    public void AddAmmoAmount(int amount) {
        ammoCount += amount;
        OnAmmoCountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetAmmoCount() {
        return ammoCount;
    }

    private void OnTriggerEnter(Collider collider) {
        HealthPickup healthPickup = collider.GetComponent<HealthPickup>();
        if (healthPickup != null) {
            Heal(50);
            healthPickup.DestroySelf();
        }

        AmmoPickup ammoPickup = collider.GetComponent<AmmoPickup>();
        if (ammoPickup != null) {
            AddAmmoAmount(10);
            ammoPickup.DestroySelf();
        }

        StarPickup starPickup = collider.GetComponent<StarPickup>();
        if (starPickup != null) {
            starPickup.DestroySelf();
        }
    }

}
