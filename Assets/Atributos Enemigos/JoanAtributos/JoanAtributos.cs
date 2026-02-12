using UnityEngine;
using UnityEngine.Splines;

public class JoanAtributos : EnemigoBase
{
    private SplineAnimate splineAnim;
    private float velocidadBase;
    private bool faseMedio = false;
    private bool faseRapido = false;

    public override void Start()
    {
        base.Start();
        splineAnim = GetComponent<SplineAnimate>();
        // Guardamos la velocidad inicial que configuraste en el inspector
        velocidadBase = splineAnim.MaxSpeed;
    }

    void Update()
    {
        float ratioVida = _vidaActual / vidaMaxima;

        // Fase Rápida (15% de vida)
        if (ratioVida <= 0.15f && !faseRapido)
        {
            splineAnim.MaxSpeed = velocidadBase * 2.5f; // Más del doble de velocidad
            faseRapido = true;
            Debug.Log("¡Joan entra en Furia total!");
        }
        // Fase Media (50% de vida)
        else if (ratioVida <= 0.5f && !faseMedio && !faseRapido)
        {
            splineAnim.MaxSpeed = velocidadBase * 1.6f; // Un 60% más rápido
            faseMedio = true;
            Debug.Log("¡Joan se está cabreando!");
        }
    }
}