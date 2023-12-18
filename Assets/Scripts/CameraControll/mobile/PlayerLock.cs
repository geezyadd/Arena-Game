using UnityEngine;

public class PlayerLock : MonoBehaviour
{
    [SerializeField] private Transform playerBody; // Ссылка на трансформ игрока
    [SerializeField] private float sensitivity = 100f; // Чувствительность вращения камеры
    private float xRotation = 0f; // Угол поворота по оси X
    [SerializeField] private bool lockCursor; // Флаг для блокировки курсора

    [SerializeField] private FixedTouchField fixedTouchField; // Ссылка на компонент для обработки сенсорных вводов
    private Vector2 touchInput; // Ось ввода для блокировки курсора

    private void Start()
    {
        if (lockCursor) Cursor.lockState = CursorLockMode.Locked; // Если флаг установлен, блокируем курсор
    }

    private void Update()
    {
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        touchInput = fixedTouchField.TouchDist; // Получаем входные данные сенсорного ввода

        float mouseX = touchInput.x * sensitivity * Time.deltaTime; // Рассчитываем величину вращения по оси X
        float mouseY = touchInput.y * sensitivity * Time.deltaTime; // Рассчитываем величину вращения по оси Y

        xRotation -= mouseY; // Применяем вращение по оси Y
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничиваем угол вращения по оси Y

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Применяем вращение к самой камере
        playerBody.Rotate(Vector3.up * mouseX); // Вращаем тело игрока по горизонтали
    }
}