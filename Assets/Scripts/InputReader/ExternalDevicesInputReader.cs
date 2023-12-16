using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalDevicesInputReader : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;
    public bool Attack { get; private set; }
    public float GetHorizontalInput() { return _horizontalInput; }
    public float GetVerticalInput() { return _verticalInput; }

    private void Update()
    {
        InputSeter();
        MouseInput();
    }
    private void InputSeter()
    {
        _horizontalInput = NormalizedInput("Horizontal");
        _verticalInput = NormalizedInput("Vertical");
    }

    private int NormalizedInput(string Direction)
    {
        if (Input.GetAxis(Direction) > 0) { return 1; }
        else if (Input.GetAxis(Direction) < 0) { return -1; }
        else { return 0; }
    }

    private void MouseInput() 
    {
        if (Input.GetMouseButton(0)) 
        {
            Attack = true;
        }
        if (!Input.GetMouseButton(0))
        {
            Attack = false;
        }
    }
}


