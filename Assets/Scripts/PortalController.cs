using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public int requiredMona = 3; // Сколько монет нужно для открытия

    private SpriteRenderer spriteRenderer;
    private Collider2D trigger;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trigger = GetComponent<Collider2D>();

        // Изначально скрыт
        SetActive(false);
    }

    // Вызывается из PlayerController, когда Mona обновляется
    public void CheckAndOpen(int currentMona)
    {
        if (currentMona >= requiredMona)
        {
            SetActive(true);
        }
    }

    private void SetActive(bool active)
    {
        if (spriteRenderer != null) spriteRenderer.enabled = active;
        if (trigger != null) trigger.enabled = active;
        gameObject.SetActive(true); // чтобы Update/OnTrigger работали
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var win = FindObjectOfType<SimpleWin>();
            if (win != null)
            {
                win.TriggerWin();
            }
            else
            {
                // Резерв: сразу перезапуск (на случай, если SimpleWin не найден)
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}