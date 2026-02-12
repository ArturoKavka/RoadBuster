using UnityEngine;

public class BuildableSpot : MonoBehaviour
{
    [Header("Estado")]
    public bool isBuilt = false;
    private GameObject turretInstance;

    public void BuildTurret(TurretBlueprint blueprint)
    {
        if (isBuilt) return;

        if (blueprint != null && blueprint.prefab != null)
        {
            Vector3 spawnPos = transform.position + Vector3.up * 1.05f;
            turretInstance = Instantiate(blueprint.prefab, spawnPos, Quaternion.identity);

            isBuilt = true;

            Debug.Log("Torreta construida: " + blueprint.turretName);
        }
    }
}