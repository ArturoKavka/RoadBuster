using UnityEngine;

public class Seleccionar : MonoBehaviour
{
    Color c_original;

    private void Start()
    {
        c_original = GetComponent<Renderer>().material.color;
    }

    private void OnMouseDown()
    {
        Debug.Log("Has seleccionado el objeto: " + gameObject.name);
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseUp()
    {
        Debug.Log("Has deseleccionado el objeto: " + gameObject.name);

        GetComponent<Renderer>().material.color = c_original;

    }
}
