using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float rotationAcceleration = 5;
    public float maxRotationSpeed = 200;
    
    public float movingAcceleration = 5;
    public float maxMovingSpeed = 200;

    private float _currentRotationSpeed = 0;
    private float _currentMovingSpeed = 0;

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            Turn(right: true);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            Turn(left: true);
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            Move(forward: true);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            Move(backward: true);

        if (!(Input.GetKey(KeyCode.RightArrow) 
              || Input.GetKey(KeyCode.D)
              || Input.GetKey(KeyCode.LeftArrow)
              || Input.GetKey(KeyCode.A)))
        {
            if (Mathf.Abs(_currentRotationSpeed) > 0)
                Turn(stopping: true);
        }
        
        if (!(Input.GetKey(KeyCode.UpArrow) 
              || Input.GetKey(KeyCode.W)
              || Input.GetKey(KeyCode.DownArrow)
              || Input.GetKey(KeyCode.S)))
        {
            if (Mathf.Abs(_currentMovingSpeed) > 0)
                Move(stopping: true);
        }
    }

    private void Turn(bool right = false, bool left = false, bool stopping = true)
    {
        if (right)
        {
            if (_currentRotationSpeed < maxRotationSpeed)
                _currentRotationSpeed += rotationAcceleration;
        }
        
        else if (left)
        {
            if (_currentRotationSpeed > -maxRotationSpeed)
                _currentRotationSpeed -= rotationAcceleration;
        }
        
        else if (stopping)
        {
            if (_currentRotationSpeed > 0)
                _currentRotationSpeed -= rotationAcceleration / 2;
            else
                _currentRotationSpeed += rotationAcceleration / 2;
            if (Mathf.Abs(_currentRotationSpeed) < 0)
                _currentRotationSpeed = 0;
        }
        
        GetComponent<Rigidbody>().angularVelocity =
            new Vector3(
                0,
                _currentRotationSpeed,
                0
            );
    }
    
    private void Move(bool forward = false, bool backward = false, bool stopping = false)
    {
        if (forward)
        {
            if (_currentMovingSpeed < maxMovingSpeed)
                _currentMovingSpeed += movingAcceleration;
        }
        
        else if (backward)
        {
            if (_currentMovingSpeed > -maxMovingSpeed)
                _currentMovingSpeed -= movingAcceleration;
        }
        
        else if (stopping)
        {
            if (_currentMovingSpeed > 0)
                _currentMovingSpeed -= movingAcceleration / 2;
            else
                _currentMovingSpeed += movingAcceleration / 2;
            if (Mathf.Abs(_currentMovingSpeed) < 0)
                _currentMovingSpeed = 0;
        }

        var forwardDirection = transform.forward;
        
        GetComponent<Rigidbody>().velocity = 
            new Vector3(
                forwardDirection.x,
                0,
                forwardDirection.z
                ) 
            * _currentMovingSpeed;
    }
}
