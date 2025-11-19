using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenuPanel; // Панель меню победы
    public Button level1Button;     // Кнопка "1 уровень"
    public Button quitButton;       // Кнопка "Выйти"

    void Start()
    {
        if (winMenuPanel != null)
            winMenuPanel.SetActive(false);

        if (level1Button != null)
            level1Button.onClick.AddListener(RestartLevel);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    public void ShowMenu()
    {
        Time.timeScale = 0f;
        if (winMenuPanel != null)
            winMenuPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ✅ Перезапуск текущей сцены
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}