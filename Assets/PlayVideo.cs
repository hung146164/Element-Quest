using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd; 
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoEnd; 
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        vp.Stop();
        vp.Play(); 
    }
}
