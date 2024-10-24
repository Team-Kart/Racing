using Cinemachine;
using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.HID;

public class KartControl : NetworkBehaviour
{
    [SerializeField] Transform vehicle;
    [SerializeField] float speed = 50;
    [SerializeField] float turnSpeed = 1;
    [SerializeField] float lerpSpeed = .1f;

    [SerializeField] CinemachineFreeLook vcam;
    [SerializeField] AudioListener listener;

    Rigidbody rb;

    bool grounded;

    //Vector2 moveInput;
    float moveInput;
    float turnInput;


    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            listener.enabled = true;
            rb = GetComponent<Rigidbody>();

            vcam.Priority = 1;
        }
        else
        {
            vcam.Priority = 0;
        }
    }
    private void FixedUpdate()
    {
        if (!IsOwner) return;

        //HandleMovementRpc(moveInput, turnInput);

        UpdateMovement(moveInput);
        UpdateVehicleDirection(turnInput);
    }
    private void UpdateMovement(float move)
    {
        //old movement, more rigid but precise
        Vector3 newVel = vehicle.forward * speed * move;
        newVel.y = rb.velocity.y;
        rb.velocity = newVel;

        //TODO:: smoother but needs further tweaking
        /*if (grounded)
            rb.AddForce(vehicle.forward * speed * moveInput, ForceMode.Force);*/

        vehicle.position = Vector3.Lerp(vehicle.position, transform.position, lerpSpeed);
    }

    //orients the vehicle object based on the ground's normal direction, gotten from a raycast
    //TODO eventually limit the amount that the kart can be rotated to simulate drifting and friction
    private void UpdateVehicleDirection(float turn)
    {
        Vector3 newRot = vehicle.rotation.eulerAngles;

        //vertical movement
        RaycastHit hit;

        if (Physics.Raycast(vehicle.position, Vector3.down, out hit, transform.localScale.y / 2 + .1f))
        {
            grounded = true;
            newRot = (Quaternion.FromToRotation(vehicle.up, hit.normal) * vehicle.rotation).eulerAngles;
        }
        else
        {
            grounded = false;
        }

        //horizontal movement
        newRot.y += turn * turnSpeed;

        vehicle.rotation = Quaternion.Lerp(vehicle.rotation, Quaternion.Euler(newRot), lerpSpeed * .5f);
    }


    //Input handlers
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
