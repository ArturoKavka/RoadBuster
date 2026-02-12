using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GestionOleadas : MonoBehaviour
{
    private const int MAXIMO_OLEADAS = 30;
    private const float DURACION_OLEADAS = 150f;
    private const float SEGUNDOS_PARA_SALTAR = 60f;
    private const float ULTIMOS_SEGUNDOS = 3f;

    [Header("Referencias UI")]
    public TextMeshProUGUI textoNumeroOleada;
    public TextMeshProUGUI textoTemporizador; // <-- Nueva referencia para el reloj

    [Header("Referencias Spawning")]
    public List<InformacionOleadas> allWaves;
    public Transform spawnPoint;

    [Header("Estado Actual")]
    public int currentWaveNumber = 0;
    public float timeRemaining;
    private bool isSpawning = false;
    private Coroutine waveTimerCoroutine;

    void Start()
    {
        // Inicializamos la primera oleada
        StartNextWave();
    }

    public void StartNextWave()
    {
        // 1. Verificar si llegamos al límite
        if (currentWaveNumber >= MAXIMO_OLEADAS)
        {
            StartInfiniteWave();
            return;
        }

        // 2. Incrementar contador y actualizar UI
        currentWaveNumber++;
        ActualizarUINumeroOleada();

        // 3. Reiniciar el tiempo
        timeRemaining = DURACION_OLEADAS;

        // 4. Detener conteos previos si existen y empezar el nuevo
        if (waveTimerCoroutine != null) StopCoroutine(waveTimerCoroutine);
        waveTimerCoroutine = StartCoroutine(WaveTimerCountdown());

        // 5. Spamear enemigos (siempre que esté en el rango de la lista)
        if (currentWaveNumber <= allWaves.Count)
        {
            StartCoroutine(SpawnEnemiesForWave(allWaves[currentWaveNumber - 1]));
        }
    }

    private IEnumerator WaveTimerCountdown()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
           
            // Actualizar el texto del reloj cada frame
            ActualizarUITemporizador(timeRemaining);

            // Lógica de "Oleada Limpia": si no hay enemigos, saltar al final
            if (!isSpawning && ControlOleadas.enemiesAliveCount <= 0 && timeRemaining > ULTIMOS_SEGUNDOS)
            {
                timeRemaining = ULTIMOS_SEGUNDOS;
            }

            yield return null;
        }

        timeRemaining = 0;
        ActualizarUITemporizador(0);
        EndWave();
    }

    private void EndWave()
    {
        Debug.Log("Oleada " + currentWaveNumber + " terminada.");
        StartNextWave();
    }

    private IEnumerator SpawnEnemiesForWave(InformacionOleadas wave)
    {
        isSpawning = true;
        foreach (EnemyGroup group in wave.enemyGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                if (timeRemaining <= 0) break;

                if (group.enemyPrefab != null)
                {
                    GameObject enemigo = Instantiate(group.enemyPrefab, spawnPoint.position, Quaternion.identity);
                    ControlOleadas.enemiesAliveCount++;
                }

                yield return new WaitForSeconds(group.spawnInterval);
            }
        }
        isSpawning = false;
    }

    // --- MÉTODOS DE APOYO (HELPERS) ---

    private void ActualizarUINumeroOleada()
    {
        if (textoNumeroOleada != null)
            textoNumeroOleada.text = "OLEADA: " + currentWaveNumber;
    }

    private void ActualizarUITemporizador(float tiempo)
    {
        if (textoTemporizador != null)
        {
            // Formatear segundos a formato MM:SS
            int minutos = Mathf.FloorToInt(tiempo / 60);
            int segundos = Mathf.FloorToInt(tiempo % 60);
            textoTemporizador.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    public void AttemptManualSkip()
    {
        // Solo permite saltar si queda mucho tiempo (según tu regla original)
        if (currentWaveNumber <= MAXIMO_OLEADAS && timeRemaining <= SEGUNDOS_PARA_SALTAR && timeRemaining > 0)
        {
            timeRemaining = ULTIMOS_SEGUNDOS;
        }
    }

    private void StartInfiniteWave()
    {
        if (waveTimerCoroutine != null) StopCoroutine(waveTimerCoroutine);
        if (textoNumeroOleada != null) textoNumeroOleada.text = "MODO INFINITO";
        if (textoTemporizador != null) textoTemporizador.text = "--:--";
       
        StartCoroutine(InfiniteSpawnLoop());
    }

    private IEnumerator InfiniteSpawnLoop()
    {
        float spawnDelay = 2.0f;
        while (true)
        {
            int randomWave = Random.Range(0, allWaves.Count);
            int randomGroup = Random.Range(0, allWaves[randomWave].enemyGroups.Count);

            GameObject prefab = allWaves[randomWave].enemyGroups[randomGroup].enemyPrefab;

            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            ControlOleadas.enemiesAliveCount++;

            yield return new WaitForSeconds(spawnDelay);
            if (spawnDelay > 0.4f) spawnDelay -= 0.02f; // Cada vez spawnean más rápido
        }
    }
}