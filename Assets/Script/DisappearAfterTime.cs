using System.Collections;
using UnityEngine;

public class DisappearAfterTime : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tiempo en segundos antes de desaparecer")]
    public float timeToDisappear = 46f;

    [Tooltip("Destruir el objeto o solo desactivarlo")]
    public bool destroyObject = false; // false = solo desactiva, true = destruye completamente

    void Start()
    {
        StartCoroutine(DisappearRoutine());
    }

    IEnumerator DisappearRoutine()
    {
        Debug.Log($"{gameObject.name} desaparecerá en {timeToDisappear} segundos...");

        // Esperar el tiempo configurado
        yield return new WaitForSeconds(timeToDisappear);

        // Desaparecer
        if (destroyObject)
        {
            Debug.Log($"{gameObject.name} destruido!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"{gameObject.name} desactivado!");
            gameObject.SetActive(false);
        }
    }
}