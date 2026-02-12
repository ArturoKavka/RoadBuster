using UnityEngine;
using TMPro;

public class EnemyInfoDisplay : MonoBehaviour
{
    public static EnemyInfoDisplay Instance { get; private set; }

    public GameObject panelInformacion;
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoVida;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        panelInformacion.SetActive(false);
    }

    public void ShowInfo(string nombre, float vidaActual, float vidaMaxima)
    {
        textoNombre.text = nombre;
        textoVida.text = $"HP: {Mathf.CeilToInt(vidaActual)} / {Mathf.CeilToInt(vidaMaxima)}";

        panelInformacion.transform.position = Input.mousePosition;

        if (!panelInformacion.activeSelf)
        {
            panelInformacion.SetActive(true);
        }
    }

    public void HideInfo()
    {
        panelInformacion.SetActive(false);
    }
}