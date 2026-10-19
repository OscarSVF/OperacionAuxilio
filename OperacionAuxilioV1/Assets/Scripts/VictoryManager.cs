using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager Instance;

    [Header("UI de Victoria")]
    public GameObject victoryPanel; // Arrastra aquí tu panel de misión cumplida
    private bool gameIsOver = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Llama a esta función cuando el jugador cumpla el objetivo (ej: recoger todos los botiquines)
    public void TriggerVictory()
    {
        if (gameIsOver) return;
        gameIsOver = true;

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        // Congela el tiempo del juego para que los enemigos o el jugador no se muevan de fondo
        Time.timeScale = 0f;
    }

    // Método para el botón "VOLVER" o reiniciar nivel
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Restablecer el tiempo antes de cambiar de escena
        // Cambia "MenuScene" por el nombre de tu escena de menú principal
        SceneManager.LoadScene("MenuScene"); 
    }
}