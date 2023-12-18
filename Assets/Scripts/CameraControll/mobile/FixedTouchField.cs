using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 touchDist; // Расстояние, пройденное пальцем на сенсорном экране
    private Vector2 pointerOld; // Предыдущая позиция пальца на сенсорном экране
    private int pointerId; // Идентификатор пальца
    private bool pressed; // Флаг для определения касания сенсорного экрана

    public Vector2 TouchDist => pressed ? touchDist : Vector2.zero; // Свойство для получения данных сенсорного ввода

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true; // Устанавливаем флаг, что произошло касание
        pointerId = eventData.pointerId; // Получаем идентификатор пальца
        pointerOld = eventData.position; // Получаем начальную позицию касания
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false; // Сбрасываем флаг касания
    }

    private void Update()
    {
        if (pressed)
        {
            if (pointerId >= 0 && pointerId < Input.touches.Length)
            {
                touchDist = Input.touches[pointerId].position - pointerOld;
                pointerOld = Input.touches[pointerId].position;
            }
            else
            {
                touchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - pointerOld;
                pointerOld = Input.mousePosition;
            }
        }
        else
        {
            touchDist = Vector2.zero;
        }
    }
}