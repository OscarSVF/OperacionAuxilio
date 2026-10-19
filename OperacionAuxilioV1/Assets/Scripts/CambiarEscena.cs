using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CambiarANombreDeEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}