using UnityEngine;

public class ExternalDevicesInputReader : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;
    public bool Attack { get; private set; }
    public bool Ultimate { get; private set; }
    public float GetHorizontalInput() { return _horizontalInput; }
    public float GetVerticalInput() { return _verticalInput; }
    private const int ATTACK_BUTTON_ID = 0;
    private void Update()
    {
        InputSeter();
        MouseInput();
        SetUltimate();
    }
    private void InputSeter()
    {
        _horizontalInput = NormalizedInput("Horizontal");
        _verticalInput = NormalizedInput("Vertical");
    }
    private void SetUltimate() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Ultimate = true;
        }
        else 
        {
            Ultimate = false;
        }
    }
    private int NormalizedInput(string Direction)
    {
        if (Input.GetAxisRaw(Direction) > 0) { return 1; }
        else if (Input.GetAxisRaw(Direction) < 0) { return -1; }
        else { return 0; }
    }
    private void MouseInput() 
    {
        if (Input.GetMouseButton(ATTACK_BUTTON_ID)) 
        {
            Attack = true;
        }
        if (!Input.GetMouseButton(ATTACK_BUTTON_ID))
        {
            Attack = false;
        }
    }
}


