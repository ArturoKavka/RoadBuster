using UnityEngine;

public interface IDanyo
{
    float vidaActual { get; }
    float vidaMaxima { get; }

    bool recibirDanyo(int cantidad, out int oroGanado);
    void morir();
}
