using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int suministrosRecolectados = 0;
    public int metaSuministros = 5; 
    public TextMeshProUGUI textoContador; // Tu Txt_MissionStatus

    [Header("Panel de Victoria")]
    public GameObject victoryPanel; // Tu panel de misión cumplida

    [Header("Menú de Pausa")]
    public GameObject pausePanel; // Tu panel de pausa
    private bool isPaused = false;

    void Awake()
    {
        // Nos aseguramos de que el tiempo corra con normalidad al iniciar la escena
        Time.timeScale = 1f;
    }
    
    void Start()
    {
        ActualizarHUD();
        
        // Asegurarnos de que los paneles empiecen ocultos al iniciar el juego
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    void Update()
    {
        // Detectar la tecla Escape para pausar / reanudar el juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void RegistrarEntrega()
    {
        suministrosRecolectados++;
        ActualizarHUD();

        if (suministrosRecolectados >= metaSuministros)
        {
            MostrarVictoria();
        }
    }

    void ActualizarHUD()
    {
        if (textoContador != null)
        {
            textoContador.text = "Suministros: " + suministrosRecolectados + " / " + metaSuministros;
        }
    }

    void MostrarVictoria()
    {
        if (textoContador != null)
        {
            textoContador.text = "¡MISIÓN CUMPLIDA! Suministros entregados.";
        }

        // Activamos el panel visual de victoria
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        // Congelamos el tiempo del juego para que nada se mueva de fondo
        Time.timeScale = 0f;

        Debug.Log("¡Misión Cumplida! Has entregado todos los suministros.");
    }

    public void PauseGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
        Time.timeScale = 0f; // Congela el juego
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        Time.timeScale = 1f; // Reanuda el juego
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Restaura el tiempo antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f; // Restaura el tiempo normal del juego
        SceneManager.LoadScene("MainMenu"); // Usa el nombre exacto que tienes en los Build Settings
    }
}