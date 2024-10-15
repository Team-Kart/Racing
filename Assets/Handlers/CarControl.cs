using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.HID;

public class CarControl : MonoBehaviour
{
    [SerializeField] Transform vehicle;
    [SerializeField] float speed = 50;
    [SerializeField] float turnSpeed = 1;
    [SerializeField] float lerpSpeed = .1f;

    Rigidbody rb;
    InputSystem input;

    bool grounded;

    //Vector2 moveInput;
    float moveInput;
    float turnInput;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = new InputSystem();

        //input.Player.Jump.performed += OnJump;
        input.Player.Enable();
        input.Player.Movement.performed += OnMove;
        input.Player.Movement.canceled += OnMove;
        input.Player.Turning.performed += OnTurn;
        input.Player.Turning.canceled += OnTurn;
    }
    private void FixedUpdate()
    {
        UpdateVehicleDirection();
        UpdateMovement();
    }

    private void UpdateVehicleDirection()
    {
        Vector3 newRot = vehicle.rotation.eulerAngles;


        

        //vertical movement
        RaycastHit hit;


        if (Physics.Raycast(vehicle.position, Vector3.down, out hit, 3))
        {
            newRot = (Quaternion.FromToRotation(vehicle.up, hit.normal) * vehicle.rotation).eulerAngles;
        }

        //horizontal movement
        if (moveInput != 0)
            newRot.y += turnInput * turnSpeed;

        vehicle.rotation = Quaternion.Lerp(vehicle.rotation, Quaternion.Euler(newRot), lerpSpeed);
    }
    private void UpdateMovement()
    {
        Vector3 newVel = vehicle.forward * speed * moveInput;
        newVel.y = rb.velocity.y;
        rb.velocity = newVel;

        vehicle.position = Vector3.Lerp(vehicle.position, transform.position, lerpSpeed*.5f);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            moveInput = 0;
            return;
        }

        moveInput = ctx.ReadValue<float>();
    }
    public void OnTurn(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            turnInput = 0;
            return;
        }

        turnInput = ctx.ReadValue<float>();
    }
}