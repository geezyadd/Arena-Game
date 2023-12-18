using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _playerRotationSpeed;
    [SerializeField] private float _playerAccelerationSpeed;
    [SerializeField] private Camera _playerCamera;
    private ExternalDevicesInputReader _inputReader;
    private Rigidbody _playerRB;
    private Vector3 _movementDirection;

    private void Start()
    {
        _inputReader = GetComponent<ExternalDevicesInputReader>();

        _playerRB = GetComponent<Rigidbody>();
    }
    

    private void FixedUpdate()
    {
        if(_inputReader.GetVerticalInput() != 0 && _inputReader.GetHorizontalInput() != 0) 
        {
            Debug.Log("ver");
            Debug.Log(_inputReader.GetVerticalInput());
            Debug.Log("hor");
            Debug.Log(_inputReader.GetHorizontalInput());
        }
        
        Movement();
        MovementDirection();
    }
    
    private void Movement()
    {
        _playerRB.MovePosition(transform.position + (_playerAccelerationSpeed * Time.deltaTime * (_movementDirection)));
    }
    private void MovementDirection() 
    {
        Vector3 cameraForwardDirection = _playerCamera.transform.forward.normalized;
        cameraForwardDirection.y = 0;
        _movementDirection = (cameraForwardDirection * _inputReader.GetVerticalInput() + _playerCamera.transform.right.normalized * _inputReader.GetHorizontalInput()).normalized;
    }
}
