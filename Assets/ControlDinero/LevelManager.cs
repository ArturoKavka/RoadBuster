using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("Configuración Económica")]
    public int dineroInicial = 500;
    public TextMeshProUGUI textoDinero;

    private int dineroActual;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        dineroActual = dineroInicial;
        ActualizarUI();
    }

    public void GanarDinero(int cantidad)
    {
        dineroActual += cantidad;
        ActualizarUI();
    }

    public bool GastarDinero(int cantidad)
    {
        if (dineroActual >= cantidad)
        {
            dineroActual -= cantidad;
            ActualizarUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ActualizarUI()
    {
        if (textoDinero != null)
        {
            textoDinero.text = "Dinero: " + dineroActual + "G";
        }
    }
}