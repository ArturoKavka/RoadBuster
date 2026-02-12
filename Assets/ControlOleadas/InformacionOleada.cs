using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NuevaOleada", menuName = "TD/Informacion de Oleada")]
public class InformacionOleadas : ScriptableObject
{
    public string nombreOleada = "Oleada 1";
    public float tiempoEsperaInicial = 3f;

    public List<EnemyGroup> enemyGroups;
}

[System.Serializable]
public struct EnemyGroup
{
    public Atributos enemyData;

    public GameObject enemyPrefab; 

    public int count; 
    public float spawnInterval; 
}