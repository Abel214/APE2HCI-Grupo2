using System.Collections;
using UnityEngine;

public class DelayedAudioPlayer : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tiempo en segundos antes de reproducir el audio")]
    public float delayTime = 46f;

    private AudioSource audioSource;

    void Start()
    {
        // Obtener el AudioSource del mismo GameObject
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No se encontró AudioSource en este GameObject!");
            return;
        }

        // Desactivar Play On Awake para que no suene al inicio
        audioSource.playOnAwake = false;

        // Iniciar la corrutina que espera y reproduce
        StartCoroutine(PlayAfterDelay());
    }

    IEnumerator PlayAfterDelay()
    {
        Debug.Log($"Audio se reproducirá en {delayTime} segundos...");

        // Esperar el tiempo configurado
        yield return new WaitForSeconds(delayTime);

        // Reproducir el audio
        audioSource.Play();

        Debug.Log("¡Audio reproducido!");
    }
}