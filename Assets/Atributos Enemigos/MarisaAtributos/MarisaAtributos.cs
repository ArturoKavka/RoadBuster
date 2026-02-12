using UnityEngine;

public class MarisaAtributos : EnemigoBase
{
    public GameObject robertoPrefab;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10f)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(robertoPrefab, transform.position + transform.forward, Quaternion.identity);
                ControlOleadas.enemiesAliveCount++;
            }
            timer = 0f;
        }
    }
}