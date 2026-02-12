using UnityEngine;
using TMPro;

public class BaseManager : MonoBehaviour
{
    public static BaseManager Instance;

    [Header("Configuración de Base")]
    public float vidaMaxima = 250f;
    public TextMeshProUGUI textoVida;

    private float vidaActual;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarUI();
    }

    public void RecibirDanyo(float cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual <= 0)
        {
            vidaActual = 0;
            GameOver();
        }
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        if (textoVida != null)
        {
            textoVida.text = "Base: " + Mathf.CeilToInt(vidaActual).ToString();
        }
    }

    private void GameOver()
    {
        Debug.Log("¡Base destruida! Juego Terminado.");
        // Aquí puedes activar un panel de derrota o reiniciar el nivel
    }
}