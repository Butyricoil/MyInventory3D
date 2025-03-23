using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private Rigidbody rb;

    public float minY = 0f; // Минимальная координата Y, ниже которой объект не может опуститься

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // Отключаем физику, если объект управляется скриптом
        }
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // Для точной обработки коллизий
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPos() + mOffset;

        // Ограничиваем позицию по оси Y
        if (newPosition.y < minY)
        {
            newPosition.y = minY;
        }

        rb.MovePosition(newPosition); // Используем MovePosition для корректной работы с физикой
    }
}