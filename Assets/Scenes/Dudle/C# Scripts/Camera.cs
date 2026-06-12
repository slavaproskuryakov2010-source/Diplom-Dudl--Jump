using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Настройки камеры")]
    [SerializeField] private Transform target;                  // Цель (игрок)
    [SerializeField] private float smoothSpeed = 0.125f;       // Плавность движения
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -10); // Смещение камеры
    [SerializeField] private bool followOnlyY = true;           // Следовать только по Y
    [SerializeField] private float minY = 0f;                  // Минимальная позиция камеры

    private Vector3 velocity = Vector3.zero;
    private float highestY;                                     // Максимальная достигнутая высота

    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        highestY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = transform.position;

        // Следуем только вверх по Y
        if (target.position.y + offset.y > highestY)
        {
            highestY = target.position.y + offset.y;
        }

        // Устанавливаем целевую позицию
        if (followOnlyY)
        {
            targetPosition.y = Mathf.Max(highestY, minY);
        }
        else
        {
            targetPosition = new Vector3(
                target.position.x + offset.x,
                Mathf.Max(target.position.y + offset.y, minY),
                offset.z
            );
        }

        // Плавное движение камеры
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }

    /// <summary>
    /// Сброс высоты камеры (например, при перезапуске)
    /// </summary>
    public void ResetCamera()
    {
        highestY = minY;
        transform.position = new Vector3(0, minY, offset.z);
    }
}  