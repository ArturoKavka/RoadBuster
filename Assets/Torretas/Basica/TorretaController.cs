using UnityEngine;

public class TorretaController : MonoBehaviour
{
    private IAtributosTorreta stats;

    public float turnSpeed = 10f;

    private Transform targetEnemy;
    private float fireCooldown;

    void Start()
    {
        stats = GetComponent<IAtributosTorreta>();

        if (stats == null)
        {
            Debug.LogError("Falta el componente IAtributosTorreta en " + gameObject.name);
            enabled = false;
            return;
        }

        fireCooldown = 0f;
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (targetEnemy == null || !IsTargetValid())
        {
            SearchForTarget();
        }

        if (targetEnemy != null)
        {
            RotateTowardsTarget();

            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / stats.velocidadDeDisparo;
            }
        }
    }

    private void SearchForTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stats.rango);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hitCollider.transform;
                }
            }
        }

        targetEnemy = closestEnemy;
    }

    private bool IsTargetValid()
    {
        if (targetEnemy == null) return false;

        float distance = Vector3.Distance(transform.position, targetEnemy.position);
        return distance <= stats.rango;
    }

    private void RotateTowardsTarget()
    {
        if (targetEnemy == null) return;

        Vector3 direction = targetEnemy.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void Shoot()
    {
        IDanyo enemigo = targetEnemy.GetComponent<IDanyo>();

        if (enemigo != null)
        {
            int oroGanado;
            bool murio = enemigo.recibirDanyo((int)stats.danyo, out oroGanado);

            Debug.Log($"{gameObject.name} golpeó a {targetEnemy.name}. Vida restante: {enemigo.vidaActual}");
        }
    }
}