using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private bool isPlaying = false;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Apagar todas las luces al inicio para que todo esté oscuro excepto la TV
        Light[] allLights = FindObjectsOfType<Light>();
        foreach (Light light in allLights)
        {
            light.enabled = false;
        }

        // Agregar evento para cuando el video termine
        videoPlayer.loopPointReached += OnVideoEnded;

        // Reproducir el video automáticamente al inicio
        PlayVideo();
    }

    private void OnVideoEnded(VideoPlayer vp)
    {
        // Activar todas las luces después de que el video termine
        Light[] allLights = FindObjectsOfType<Light>();
        foreach (Light light in allLights)
        {
            light.enabled = true;
            light.intensity = 1f; // Ajustar intensidad si es necesario
        }
        Debug.Log("Luces activadas después del video");
    }

    public void TogglePlayPause()
    {
        if (isPlaying)
        {
            videoPlayer.Pause();
            isPlaying = false;
            Debug.Log("Video pausado");
        }
        else
        {
            videoPlayer.Play();
            isPlaying = true;
            Debug.Log("Video reproduciendo");
        }
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        isPlaying = true;
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
        isPlaying = false;
    }
}