using UnityEngine;

public class HugoAtributos : EnemigoBase
{
    public float vidaEscudo = 50f;

    public override bool recibirDanyo(int cantidad, out int oroGanado)
    {
        if (vidaEscudo > 0)
        {
            float danyoAlEscudo = Mathf.Min(cantidad, vidaEscudo);
            vidaEscudo -= danyoAlEscudo;
            oroGanado = 0;

            if (vidaEscudo <= 0)
            {
                UnityEngine.Debug.Log("¡Escudo de Hugo destruido!");
            }

            return false;
        }

        return base.recibirDanyo(cantidad, out oroGanado);
    }
}