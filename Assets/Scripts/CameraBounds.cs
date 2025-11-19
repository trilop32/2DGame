using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Transform target;           // Персонаж
    public Vector2 minPosition;        // Минимальные координаты камеры (левый нижний угол)
    public Vector2 maxPosition;        // Максимальные координаты камеры (правый верхний угол)

    private void LateUpdate()
    {
        if (target == null) return;

        // Позиция камеры = позиция персонажа + смещение (как в CameraFollow)
        Vector3 desiredPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        // Ограничиваем по X и Y
        float clampedX = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);

        // Применяем ограничения
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}