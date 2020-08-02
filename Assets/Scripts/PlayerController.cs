﻿using UnityEngine;
using PhysicsClass = MyPhysics.PlayerPhysics;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public PhysicsClass Physics;
    public CharacterController characterController;
    [Range(0f, 20f)] public float speed = 10f;

    [Header("Jump")]
    public float jumpForce = 1.0f;

    [Header("Camera ajusment")]
    public Transform cam;
    [HideInInspector] private float _turnSmoothTime = 0.1f;
    [HideInInspector] private float _turnSmoothVelocity;
    public float sensivity = 0.1f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        cam = GetComponent<Transform>().GetChild(1);
    }

    void Update()
    {
        PlayerMovement();
        PlayerJump();
    }

    void PlayerMovement()
    {
        //float x = AimCameraController.AimingOrNot(sensivity, speed);
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(x, 0f, z).normalized;

        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            speed = Input.GetKey(KeyCode.LeftShift) ? speed = 20f : speed = 10f;

            characterController.Move(movDir.normalized * speed * Time.deltaTime);
        }
    }

    void PlayerJump()
    {
        Physics.Gravity();

        if (Input.GetKeyDown("space") && Physics.isGrounded)
            Physics.velocity.y = Mathf.Sqrt(jumpForce * Physics.velocity.y * Physics.gravity);
    }
}
