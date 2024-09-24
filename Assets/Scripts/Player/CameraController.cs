using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float rotationRate = 2f;
    public SpeedOffset speedOffset;
    public Vector2 maxInputOffset = new Vector2(60, 30);

    public Vector3 testVector = Vector3.up;
    Vector2 _inputOffset = Vector2.zero;
    Quaternion _camOffset;
    PlayerController _playerController;


    Rigidbody _rb;
    Vector3 _baseOffset;

    void OnLook(InputValue value)
    {
        _inputOffset = value.Get<Vector2>();
    }

    void Awake()
    {
        _camOffset = Quaternion.identity;
        // controls =  new InputController();
        // controls.Gameplay.Camera.performed += ctx => _inputOffset = ctx.ReadValue<Vector2>();
        // controls.Gameplay.Camera.canceled += ctx => _inputOffset = Vector2.zero;
    }


    void Start()
    {
        _baseOffset = transform.position - player.transform.position;
        _rb = player.GetComponent<Rigidbody>();
        _playerController = player.GetComponent<PlayerController>();
    }

    void LateUpdate()
    {
        // Calculate movement direction
        var velocity = _rb.velocity;

        var angle = _playerController.angle;

        // Calculate input offset
        var xInput = Mathf.LerpUnclamped(0, maxInputOffset.x, _inputOffset.x);
        var yInput = Mathf.LerpUnclamped(0, maxInputOffset.y, _inputOffset.y);
        var xAngle = 13f;


        // Apply camera Rotationdws
        Quaternion targetRotation = Quaternion.Euler(xAngle - yInput, angle + xInput, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationRate);

        // Calculate and Apply Camera Position Offset
        var speed = Mathf.Clamp(velocity.magnitude, 0, 25);
        var offsetFactor = Mathf.Lerp(speedOffset.min, speedOffset.max, speed / 25);
        
        
        var x = Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad) * _baseOffset.z * offsetFactor;
        var z = Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad) * _baseOffset.z * offsetFactor;
        transform.position = player.transform.position + new Vector3(x, _baseOffset.y, z);
    }


    // void OnEnable()
    // {
    //     controls.Gameplay.Enable();
    // }
    //
    // void OnDisable()
    // {
    //     controls.Gameplay.Disable();
    // }
}


[System.Serializable]
public struct SpeedOffset
{
    public float min;
    public float max;
}