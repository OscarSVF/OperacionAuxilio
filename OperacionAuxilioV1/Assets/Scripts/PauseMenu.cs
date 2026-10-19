using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Panel de Pausa")]
    public GameObject pausePanel;
    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    void Update()
{
    // Detectar si se presiona la tecla Escape
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        Debug.Log("¡Se presionó la tecla Escape!"); // Esto saldrá en la consola
        
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Restaura el tiempo normal
        isPaused = false;
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Congela el juego
        isPaused = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Asegurar restaurar el tiempo antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Asegurar restaurar el tiempo antes de cambiar de escena
        SceneManager.LoadScene("MainMenu"); // Asegúrate de que tu menú se llame así
    }
}