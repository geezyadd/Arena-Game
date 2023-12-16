using System.Collections;
using System.Collections.Generic;
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
        // Получаем ввод от мыши
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Вращаем персонажа в горизонтальной плоскости
        transform.parent.Rotate(Vector3.up * mouseX * _sensitivity);

        // Вращаем камеру в вертикальной плоскости
        rotationX -= mouseY * _sensitivity;
        rotationX = Mathf.Clamp(rotationX, -_maxYAngle, _maxYAngle);
        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
    }
}
