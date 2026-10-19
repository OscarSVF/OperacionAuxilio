using UnityEngine;

public class SuministroRecoleccion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Solo ejecuta si el que lo toca tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // Dispara la animación en el jugador
            Animator playerAnim = collision.GetComponent<Animator>();
            if (playerAnim != null)
            {
                playerAnim.SetTrigger("Recoger");
            }

            // Suma el punto usando tu GameManager
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.RegistrarEntrega();
            }

            // Dispara la pantalla de victoria
            if (VictoryManager.Instance != null)
            {
                VictoryManager.Instance.TriggerVictory();
            }

            // Destruye el botiquín
            Destroy(gameObject);
        }
    }
}