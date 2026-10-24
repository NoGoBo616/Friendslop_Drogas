using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Control_Luces : MonoBehaviour
{
    [SerializeField] float tempo;
    [SerializeField] Light2D luz;
    [SerializeField] Color[] colores;
    [SerializeField] int colorIndex;

    private void Start()
    {
        StartCoroutine(CambiarColor());
    }

    private void Update()
    {
        luz.color = colores[colorIndex];
    }
    IEnumerator CambiarColor()
    {
        colorIndex++;
        if (colorIndex >= colores.Length)
        {
            colorIndex = 0;
        }
        yield return new WaitForSeconds(tempo);
        StartCoroutine(CambiarColor());
    }
}
