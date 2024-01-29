using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField] private float _sensitivity; // Чувствительность мыши
    [SerializeField] private float _maxYAngle; // Максимальный угол вращения по вертикали

    private float rotationX = 0.0f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        CameraMove();
    }
    
    private void CameraMove() 
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
    
        transform.parent.Rotate(Vector3.up * mouseX * _sensitivity);
    
        rotationX -= mouseY * _sensitivity;
        rotationX = Mathf.Clamp(rotationX, -_maxYAngle, _maxYAngle);
        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
    }
}
