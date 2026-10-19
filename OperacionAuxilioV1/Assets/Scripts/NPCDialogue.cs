using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("Configuracion del NPC")]
    [SerializeField] private string npcName = "Rescatista Guia";
    [TextArea(2, 5)]
    [SerializeField] private string[] dialogueLines;

    [Header("Referencias de Interfaz (UI)")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text contentText;
    [SerializeField] private GameObject interactionPrompt;

    private bool isPlayerInRange = false;
    private bool isDialogueActive = false;
    private int currentLineIndex = 0;

    private void Start()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (interactionPrompt != null) interactionPrompt.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                NextDialogueLine();
            }
        }
    }

    private void StartDialogue()
    {
        if (dialogueLines == null || dialogueLines.Length == 0) return;

        isDialogueActive = true;
        currentLineIndex = 0;
        if (dialoguePanel != null) dialoguePanel.SetActive(true);
        if (interactionPrompt != null) interactionPrompt.SetActive(false);

        if (nameText != null) nameText.text = npcName;
        if (contentText != null) contentText.text = dialogueLines[currentLineIndex];
    }

    private void NextDialogueLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            if (contentText != null) contentText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (interactionPrompt != null && isPlayerInRange) interactionPrompt.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (!isDialogueActive && interactionPrompt != null)
                interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            EndDialogue();
            if (interactionPrompt != null) interactionPrompt.SetActive(false);
        }
    }
}