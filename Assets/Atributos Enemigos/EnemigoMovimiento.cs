using UnityEngine;
using UnityEngine.Splines;

public class EnemigoMovimiento : MonoBehaviour
{
    private SplineAnimate splineAnimate;
    private EnemigoBase enemigoBase;

    void Start()
    {
        splineAnimate = GetComponent<SplineAnimate>();
        enemigoBase = GetComponent<EnemigoBase>();

        GameObject rutaObj = GameObject.FindGameObjectWithTag("Ruta");

        if (rutaObj != null && splineAnimate != null)
        {
            SplineContainer container = rutaObj.GetComponent<SplineContainer>();
            if (container != null)
            {
                splineAnimate.Container = container;
                float vel = (enemigoBase != null) ? enemigoBase.ObtenerVelocidad() : 1f;
                splineAnimate.Duration = Mathf.Max(1f, 20f / vel);
                splineAnimate.Loop = SplineAnimate.LoopMode.Once;
                splineAnimate.Play();
            }
        }
    }

    void Update()
    {
        if (splineAnimate != null && !splineAnimate.IsPlaying && splineAnimate.NormalizedTime >= 1f)
        {
            if (enemigoBase != null)
            {
                enemigoBase.ReachedBase();
            }
        }
    }
}