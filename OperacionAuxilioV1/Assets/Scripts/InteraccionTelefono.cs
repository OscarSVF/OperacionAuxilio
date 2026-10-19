using UnityEngine;

public class InteraccionTelefono : MonoBehaviour
{
    private bool jugadorCerca = false;

    void Update()
    {
        // Si el jugador está cerca y presiona la letra E del teclado
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            UsarTelefono();
        }
    }

    // Esto detecta cuando el jugador entra al área de la cabina
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("¡Acercate y presiona 'E' para usar la cabina!");
        }
    }

    // Esto detecta cuando el jugador se aleja
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
            Debug.Log("Te alejaste de la cabina.");
        }
    }

    void UsarTelefono()
    {
        // AQUÍ ESCRIBES LO QUE QUIERES QUE PASE AL PRESIONAR LA E
        Debug.Log("¡Ring ring! Has usado la cabina de emergencia de Operación Auxilio.");
        
        // (Opcional) Si quieres que te sume un mensaje, abra un diálogo o te dé una pista, lo puedes poner aquí
    }
}