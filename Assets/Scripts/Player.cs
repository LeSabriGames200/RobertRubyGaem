using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public float walkingSpeed = 10f;
    public float runningSpeed = 16f;
    public float stamina = 100f;
    public float staminaDecreaseRate = 20f;
    public float staminaIncreaseRate = 10f;
    public float maxStamina = 100f;
    public Slider staminaBar;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public GameObject warning;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    public bool isRunning = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set the max value of the stamina bar
        staminaBar.maxValue = maxStamina;
        lookSpeed = PlayerPrefs.GetFloat("MouseSensitivity");
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && canMove)
        {
            isRunning = true;
            stamina -= staminaDecreaseRate * Time.deltaTime;
        }
        else
        {
            isRunning = false;
            if (stamina < maxStamina)
            {
                stamina += staminaIncreaseRate * Time.deltaTime;
            }
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        staminaBar.value = stamina;

        // Adjust speed based on stamina and running state
        float currentSpeed = isRunning && stamina > 1 ? runningSpeed : walkingSpeed;
        float curSpeedX = canMove ? currentSpeed * Input.GetAxisRaw("Vertical") : 0;
        float curSpeedY = canMove ? currentSpeed * Input.GetAxisRaw("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxisRaw("Mouse X") * lookSpeed, 0);
        }

        if (stamina <= 1)
        {
            warning.SetActive(true);
            if (isRunning)
            {
                isRunning = false;
                canMove = true;
            }
        }
        else
        {
            warning.SetActive(false);
        }
    }


    public void RechargeStamina(float amount)
    {
        stamina = Mathf.Clamp(stamina + amount, 0, maxStamina);
        staminaBar.value = stamina;
    }

}
