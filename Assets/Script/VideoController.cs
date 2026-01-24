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