using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f; // Плавность
    public Vector2 offset = new Vector2(0f, 0.5f); // Ближе к персонажу

    public Vector2 minPosition; // Границы камеры
    public Vector2 maxPosition;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );

        // Плавное перемещение
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Ограничение по границам
        float clampedX = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}