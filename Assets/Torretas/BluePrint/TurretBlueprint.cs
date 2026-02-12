using UnityEngine;

[CreateAssetMenu(fileName = "NuevaTorreta", menuName = "TD/Torreta Blueprint")]
public class TurretBlueprint : ScriptableObject
{
    public GameObject prefab;
    public int cost;
    public string turretName = "Torreta Base";
    public Sprite turretIcon; 
}