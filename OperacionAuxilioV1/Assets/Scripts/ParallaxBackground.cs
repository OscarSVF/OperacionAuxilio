using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform mainCamera;
    private Vector3 lastCameraPosition;
    [SerializeField] private float parallaxEffectMultiplier = 0.5f; // Qué tan rápido se mueve el fondo (0 = estático, 1 = se mueve igual que la cámara)

    void Start()
    {
        mainCamera = Camera.main.transform;
        lastCameraPosition = mainCamera.position;
    }

    void LateUpdate()
    {
        // Calcula cuánto se movió la cámara
        Vector3 deltaMovement = mainCamera.position - lastCameraPosition;
        
        // Mueve el fondo basado en el movimiento de la cámara multiplicado por la velocidad de paralaje
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y * parallaxEffectMultiplier, 0);
        
        lastCameraPosition = mainCamera.position;
    }
}