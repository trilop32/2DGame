using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleWin : MonoBehaviour
{
    public GameObject winScreen; // Панель WinScreen
    public float autoRestartDelay = 3f;

    public void TriggerWin()
    {
        Time.timeScale = 0f;
        if (winScreen != null)
            winScreen.SetActive(true);

        Invoke("RestartScene", autoRestartDelay);
    }

    private void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}