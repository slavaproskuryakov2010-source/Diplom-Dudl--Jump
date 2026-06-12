using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Doodle : MonoBehaviour
{
    [Header("Движение")]
    [SerializeField] private float moveSpeed = 5f;              // Скорость движения
    [SerializeField] private float jumpForce = 10f;            // Сила прыжка

    [Header("Физика")]
    [SerializeField] private float gravityScale = 3f;          // Гравитация
    [SerializeField] private float maxFallSpeed = -15f;        // Максимальная скорость падения

    [Header("Телепортация")]
    [SerializeField] private bool wrapAround = true;            // Телепортация по краям

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    private Vector2 screenBounds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Настройка физики
        rb.gravityScale = gravityScale;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Получаем границы экрана
        Camera cam = Camera.main;
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
    }

    void Update()
    {
        // Получаем ввод
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Поворот спрайта
        if (spriteRenderer != null)
        {
            if (horizontalInput > 0)
                spriteRenderer.flipX = false;
            else if (horizontalInput < 0)
                spriteRenderer.flipX = true;
        }

        // Телепортация по краям
        if (wrapAround)
        {
            Vector3 pos = transform.position;

            if (pos.x > screenBounds.x)
            {
                pos.x = -screenBounds.x;
                transform.position = pos;
            }
            else if (pos.x < -screenBounds.x)
            {
                pos.x = screenBounds.x;
                transform.position = pos;
            }
        }
    }

    void FixedUpdate()
    {
        // Горизонтальное движение
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Ограничение падения
        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
    }

    // Автоматический прыжок при касании платформы
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем что персонаж падает вниз
        if (rb.linearVelocity.y <= 0)
        {
            // Проверяем что касание сверху платформы
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    Jump();
                    break;
                }
            }
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
}