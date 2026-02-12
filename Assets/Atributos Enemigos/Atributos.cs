using UnityEngine;

[CreateAssetMenu(fileName = "NuevosStats", menuName = "RB/Atributos")] 
public class Atributos : ScriptableObject
{
    public string nombre;
    public float velocidad;
    public float vidaMaxima;

    public int goldReward;
    public int damageToBase;
}