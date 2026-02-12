using UnityEngine;

public class EnemigoBase : MonoBehaviour, IDanyo
{
    [SerializeField] protected Atributos estadisticas;

    protected float _vidaActual;
    private const int GOLD_PER_DAMAGE_POINT = 1;

    public float vidaActual => _vidaActual;
    public float vidaMaxima => estadisticas != null ? estadisticas.vidaMaxima : 0f;

    public virtual void Start()
    {
        if (!CompareTag("Enemy")) gameObject.tag = "Enemy";

        if (estadisticas == null)
        {
            enabled = false;
            return;
        }

        _vidaActual = estadisticas.vidaMaxima;
    }

    public virtual bool recibirDanyo(int cantidad, out int oroGanado)
    {
        float danyoReal = Mathf.Min(cantidad, _vidaActual);
        _vidaActual -= danyoReal;

        oroGanado = Mathf.RoundToInt(danyoReal * GOLD_PER_DAMAGE_POINT);

        if (LevelManager.main != null)
        {
            LevelManager.main.GanarDinero(oroGanado);
        }

        if (_vidaActual <= 0)
        {
            morir();
            return true;
        }

        return false;
    }

    public virtual void morir()
    {
        if (ControlOleadas.enemiesAliveCount > 0)
        {
            ControlOleadas.enemiesAliveCount--;
        }
        Destroy(gameObject);
    }

    public virtual void ReachedBase()
    {
        if (BaseManager.Instance != null)
        {
            BaseManager.Instance.RecibirDanyo(_vidaActual);
        }

        if (ControlOleadas.enemiesAliveCount > 0)
        {
            ControlOleadas.enemiesAliveCount--;
        }
        Destroy(gameObject);
    }

    protected virtual void OnMouseEnter()
    {
        if (EnemyInfoDisplay.Instance != null && estadisticas != null)
        {
            EnemyInfoDisplay.Instance.ShowInfo(estadisticas.nombre, _vidaActual, vidaMaxima);
        }
    }

    protected virtual void OnMouseExit()
    {
        if (EnemyInfoDisplay.Instance != null) EnemyInfoDisplay.Instance.HideInfo();
    }

    public float ObtenerVelocidad()
    {
        return estadisticas != null ? estadisticas.velocidad : 0f;
    }
}