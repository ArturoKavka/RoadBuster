using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemigoEnOleada
{
    public GameObject enemigoPrefab;
    public int cantidad;
}

[System.Serializable]
public class Oleada
{
    public string nombreOleada;
    public List<EnemigoEnOleada> enemigos;
    public float tiempoEntreSpawns;
}

public class ControlOleadas : MonoBehaviour
{
    public static int enemiesAliveCount = 0;

    [Header("Configuración")]
    public Transform spawnPoint;
    public float tiempoEntreOleadas = 5f;
    public List<Oleada> todasLasOleadas;

    private int oleadaActualIndex = 0;
    private bool esperandoSiguienteOleada = false;

    void Update()
    {
        if (esperandoSiguienteOleada) return;

        // Si no quedan enemigos y no hemos terminado todas las oleadas
        if (enemiesAliveCount <= 0 && oleadaActualIndex < todasLasOleadas.Count)
        {
            StartCoroutine(SpawnOleada());
        }
    }

    IEnumerator SpawnOleada()
    {
        esperandoSiguienteOleada = true;
        yield return new WaitForSeconds(tiempoEntreOleadas);

        Oleada oleada = todasLasOleadas[oleadaActualIndex];
        Debug.Log("Iniciando: " + oleada.nombreOleada);

        foreach (EnemigoEnOleada grupo in oleada.enemigos)
        {
            for (int i = 0; i < grupo.cantidad; i++)
            {
                SpawnEnemigo(grupo.enemigoPrefab);
                yield return new WaitForSeconds(oleada.tiempoEntreSpawns);
            }
        }

        oleadaActualIndex++;
        esperandoSiguienteOleada = false;
    }

    void SpawnEnemigo(GameObject prefab)
    {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        enemiesAliveCount++;
    }
}