using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 8f; // Fuerza del salto

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;

    // --- 1. NUEVA VARIABLE PARA BLOQUEAR CONTROLES ---
    private bool estaBloqueado = false;
     
    private void Start()
    {
        // Forzamos a apagar cualquier trigger retenido al iniciar
        if (animator != null)
        {
            animator.ResetTrigger("Recoger");
            animator.ResetTrigger("Saltar");
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Si está bloqueado recogiendo el botiquín, ignoramos el teclado
        if (estaBloqueado) return;

        // 1. Movimiento horizontal (ignoramos la flecha abajo)
        float horizontal = Input.GetAxisRaw("Horizontal");

        // 2. Control del salto (Con un solo toque usando Espacio o Flecha Arriba)
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Solo permite saltar si la velocidad vertical es casi 0 (para no saltar en el aire)
            if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

                // Disparamos la animación de saltar
                if (animator != null)
                {
                    animator.SetTrigger("Saltar");
                }
            }
        }

        // 3. Voltear el sprite
        if (horizontal > 0 && !facingRight) Flip();
        else if (horizontal < 0 && facingRight) Flip();

        // 4. Animación de correr
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontal));
        }
    }

    private void FixedUpdate()
    {
        // --- 2. NUEVO: FRENA SU VELOCIDAD SI ESTÁ BLOQUEADO ---
        if (estaBloqueado)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        // Aplicamos velocidad horizontal y respetamos la gravedad natural en Y
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que tocamos tiene la etiqueta "Botiquin"
        if (collision.CompareTag("Botiquin"))
        {
            // --- 3. NUEVO: BLOQUEAR, ANIMAR, SUMAR Y DESBLOQUEAR DESPUÉS ---
            estaBloqueado = true;

            // 1. Activar la animación de recoger en el Animator
            if (animator != null)
            {
                animator.SetTrigger("Recoger");
            }

            // 2. Avisar al GameManager para que sume 1 en el texto
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.RegistrarEntrega();
            }

            // 3. Destruir (desaparecer) el botiquín de la pantalla
            Destroy(collision.gameObject);

            // 4. Desbloquear los controles tras 0.6 segundos (puedes ajustar este tiempo según lo que dure tu animación)
            Invoke("DesbloquearMovimiento", 0.6f);
        }
    }

    private void DesbloquearMovimiento()
    {
        estaBloqueado = false;
    }
}