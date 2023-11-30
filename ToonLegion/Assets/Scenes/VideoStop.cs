using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoStop : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        videoPlayer.enabled = false; // Disable the Video Player
        rawImage.enabled = false; // Disable the Raw Image
    }
}
