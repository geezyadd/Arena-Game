using UnityEngine;

public class PlayerLock : MonoBehaviour
{
    [SerializeField] private Transform playerBody; // ������ �� ��������� ������
    [SerializeField] private float sensitivity = 100f; // ���������������� �������� ������
    private float xRotation = 0f; // ���� �������� �� ��� X
    [SerializeField] private bool lockCursor; // ���� ��� ���������� �������

    [SerializeField] private FixedTouchField fixedTouchField; // ������ �� ��������� ��� ��������� ��������� ������
    private Vector2 touchInput; // ��� ����� ��� ���������� �������

    private void Start()
    {
        if (lockCursor) Cursor.lockState = CursorLockMode.Locked; // ���� ���� ����������, ��������� ������
    }

    private void Update()
    {
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        touchInput = fixedTouchField.TouchDist; // �������� ������� ������ ���������� �����

        float mouseX = touchInput.x * sensitivity * Time.deltaTime; // ������������ �������� �������� �� ��� X
        float mouseY = touchInput.y * sensitivity * Time.deltaTime; // ������������ �������� �������� �� ��� Y

        xRotation -= mouseY; // ��������� �������� �� ��� Y
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ������������ ���� �������� �� ��� Y

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // ��������� �������� � ����� ������
        playerBody.Rotate(Vector3.up * mouseX); // ������� ���� ������ �� �����������
    }
}