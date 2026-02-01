using System.Collections;
using UnityEngine;

public class LightAfterTime : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tiempo en segundos antes de encender la luz")]
    public float timeToLight = 46f;

    [Tooltip("Luz que se encenderá (arrastra aquí tu Light)")]
    public Light lightToActivate;

    [Tooltip("Intensidad final de la luz")]
    public float targetIntensity = 3f;

    [Tooltip("Velocidad de encendido (0 = instantáneo)")]
    public float fadeInDuration = 2f;

    [Header("Audio Configuration")]
    [Tooltip("Tiempo antes de reproducir el audio (en segundos)")]
    public float timeToPlayAudio = 44f;

    [Tooltip("AudioSource del efecto de luz")]
    public AudioSource audioSource;

    [Header("Timer y Cambio de Color")]
    [Tooltip("Tiempo en segundos antes de cambiar la luz a rojo (60 = 1 minuto)")]
    public float timeToRedLight = 90f;

    [Tooltip("Color rojo para la luz")]
    public Color redColor = Color.red;

    [Tooltip("Duración del cambio de color (en segundos)")]
    public float colorTransitionDuration = 1f;

    private Color originalColor;

    void Start()
    {
        if (lightToActivate == null)
        {
            lightToActivate = GetComponent<Light>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (lightToActivate != null)
        {
            // Guardar el color original
            originalColor = lightToActivate.color;

            // Apagar la luz al inicio
            lightToActivate.intensity = 0f;
            lightToActivate.enabled = true;
        }

        // Iniciar corrutinas
        StartCoroutine(PlayAudioAfterDelay());
        StartCoroutine(TurnOnLightAfterDelay());
        StartCoroutine(ChangeToRedAfterDelay());
    }

    IEnumerator PlayAudioAfterDelay()
    {
        Debug.Log($"El audio se reproducirá en {timeToPlayAudio} segundos...");

        yield return new WaitForSeconds(timeToPlayAudio);

        if (audioSource != null)
        {
            audioSource.Play();
            Debug.Log("¡Audio reproducido!");
        }
        else
        {
            Debug.LogWarning("No hay AudioSource asignado");
        }
    }

    IEnumerator TurnOnLightAfterDelay()
    {
        Debug.Log($"La luz se encenderá en {timeToLight} segundos...");

        yield return new WaitForSeconds(timeToLight);

        Debug.Log("¡Encendiendo luz!");

        if (fadeInDuration > 0)
        {
            // Encender gradualmente
            float elapsed = 0f;
            while (elapsed < fadeInDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / fadeInDuration;
                lightToActivate.intensity = Mathf.Lerp(0f, targetIntensity, t);
                yield return null;
            }
        }

        lightToActivate.intensity = targetIntensity;
        Debug.Log("¡Luz encendida completamente!");
    }

    IEnumerator ChangeToRedAfterDelay()
    {
        Debug.Log($"La luz cambiará a rojo en {timeToRedLight} segundos...");

        yield return new WaitForSeconds(timeToRedLight);

        Debug.Log("¡Cambiando luz a rojo!");

        if (lightToActivate != null)
        {
            if (colorTransitionDuration > 0)
            {
                // Cambiar color gradualmente
                float elapsed = 0f;
                while (elapsed < colorTransitionDuration)
                {
                    elapsed += Time.deltaTime;
                    float t = elapsed / colorTransitionDuration;
                    lightToActivate.color = Color.Lerp(originalColor, redColor, t);
                    yield return null;
                }
            }

            lightToActivate.color = redColor;
            Debug.Log("¡Luz ahora es roja!");
        }
    }
}