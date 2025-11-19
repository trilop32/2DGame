using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public int health = 3;
    public int Mona = 0;
    public int maxMonaToWin = 3; // количество монет для победы

    public TMP_Text healthText;
    public TMP_Text monaText;
    public GameObject deathScreen;
    public GameObject winScreen;

    public AudioSource deathSound;
    public AudioSource backgroundMusic;
    public TMP_Text winText;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isGrounded;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = IsGrounded();
        UpdateUI();
    }
    private bool IsGrounded()
    {
        Collider2D col = GetComponent<Collider2D>();
        Vector2 size = col.bounds.size;
        Vector2 center = col.bounds.center;

        // Луч чуть короче, чем половина высоты — чтобы не цеплять стенки
        float skinWidth = 0.05f;
        float distance = size.y / 2 + skinWidth;

        RaycastHit2D hit = Physics2D.Raycast(
            center - new Vector2(0, size.y / 2 - skinWidth),
            Vector2.down,
            distance,
            groundLayer
        );

        // Визуализация для отладки (включите в Scene View → Gizmos)
        Debug.DrawRay(
            center - new Vector2(0, size.y / 2 - skinWidth),
            Vector2.down * distance,
            hit.collider ? Color.green : Color.red
        );

        return hit.collider != null;
    }
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
    }
    public PortalController portal; // ← ДОБАВЬТЕ ЭТО

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("💥 Столкновение с: " + collision.gameObject.name + " (тег: " + collision.tag + ")");

        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            health--;
            UpdateUI();
            if (health <= 0)
            {
                Die();
            }
        }
        if (collision.CompareTag("Moneta"))
        {
            Mona++;
            UpdateUI();
            collision.gameObject.SetActive(false);

            // 🔑 Проверяем и открываем портал
            if (portal != null)
            {
                portal.CheckAndOpen(Mona);
            }

            if (Mona >= maxMonaToWin)
            {
                FindObjectOfType<WinMenu>()?.ShowMenu();
            }
        }
    }

    public void Die()
    {
        if (deathSound != null)
            backgroundMusic.Stop();
        //deathSound.Play();
        Invoke("RestartGame", 0.2f);
    }

    private void Win()
    {
        Debug.Log("🎉 ПОБЕДА! Собрано монет: " + Mona);

        if (winScreen != null)
            winScreen.SetActive(true); // ← Это должно включить панель
        else
            Debug.LogError("❌ winScreen не привязан!");

        if (winText != null)
            winText.gameObject.SetActive(true); // ← Это должно включить текст
        else
            Debug.LogError("❌ winText не привязан!");

        Time.timeScale = 0f;

        // Автоматический рестарт через 3 секунды
        Invoke("RestartGame", 3f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void UpdateUI()
    {
        if (healthText != null) healthText.text = "Health: " + health;
        if (monaText != null) monaText.text = "Mona: " + Mona;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}