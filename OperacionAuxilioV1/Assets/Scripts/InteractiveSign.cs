using UnityEngine;
using TMPro;

public class InteractiveSign : MonoBehaviour
{
    [Header("Información del Cartel (Educativa)")]
    public string signTitle = "Punto de Evacuación UTS";
    [TextArea(2, 4)]
    public string signMessage = "Punto de encuentro temporal. Mantenga la calma y siga las rutas de evacuación señalizadas hacia el centro de acopio.";

    [Header("Referencias de UI")]
    public GameObject dialoguePanel;   // Arrastra aquí tu DialoguePanel
    public TextMeshProUGUI nameText;   // Arrastra aquí el texto del título/nombre
    public TextMeshProUGUI contentText;// Arrastra aquí el texto del contenido

    private bool playerInRange = false;

    void Update()
    {
        // Si el jugador está cerca, presiona E y el panel está asignado
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowSignInfo();
        }
    }

    void ShowSignInfo()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
            if (nameText != null) nameText.text = signTitle;
            if (contentText != null) contentText.text = signMessage;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            // Opcional: Ocultar el panel al alejarse
            if (dialoguePanel != null) dialoguePanel.SetActive(false);
        }
    }
}