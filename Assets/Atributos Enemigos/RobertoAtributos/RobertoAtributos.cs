using UnityEngine;

public class RobertoAtributos : EnemigoBase
{
    public override bool recibirDanyo(int cantidad, out int oroGanado)
    {
        bool resultado = base.recibirDanyo(cantidad, out oroGanado);
        oroGanado = 0;
        return resultado;
    }
}
