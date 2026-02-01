using System.Collections;
using UnityEngine;

public class AppearAfterTime : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tiempo en segundos antes de aparecer")]
    public float timeToAppear = 46f;

    [Tooltip("Objetos que aparecerán (arrastra aquí los GameObjects)")]
    public GameObject[] objectsToAppear;

    void Start()
    {
        // Desactivar todos los objetos al inicio
        foreach (GameObject obj in objectsToAppear)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        StartCoroutine(AppearRoutine());
    }

    IEnumerator AppearRoutine()
    {
        Debug.Log($"Objetos aparecerán en {timeToAppear} segundos...");

        // Esperar el tiempo configurado
        yield return new WaitForSeconds(timeToAppear);

        // Activar todos los objetos
        foreach (GameObject obj in objectsToAppear)
        {
            if (obj != null)
            {
                obj.SetActive(true);
                Debug.Log($"{obj.name} apareció!");
            }
        }

        Debug.Log("¡Todos los objetos han aparecido!");
    }
}