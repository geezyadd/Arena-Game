using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInput : MonoBehaviour
{
    [SerializeField] private bl_Joystick _joystick;
    private float _horizontalInput;
    private float _verticalInput;
    public float GetHorizontalInput() { return _horizontalInput; }
    public float GetVerticalInput() { return _verticalInput; }
    private void FixedUpdate()
    {
        JoystickInputSeter();
    }
    private void JoystickInputSeter() 
    {
       _horizontalInput = NormalizedInput(_joystick.Horizontal);
       _verticalInput = NormalizedInput(_joystick.Vertical);
    }
    private float NormalizedInput(float value) 
    {
        float deadZone = 0.1f; 
        if (Mathf.Abs(value) > deadZone)
        {
            if (value > 0f)
            {
                return 1f;
            }
            if (value < 0f)
            {
                return -1f;
            }
        }
        return 0f;
    }
}
