using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string gameSceneName="Laboratory Scene";
    
    void Start()
    {
        // Add a listener for when the video finishes playing
        videoPlayer.loopPointReached += EndReached;
        // Start playing the video
        videoPlayer.Play();
    }
    
    void EndReached(VideoPlayer vp)
    {
        // Load the main game scene when the video ends
        SceneManager.LoadScene(gameSceneName);
    }
    
    // Optional: Add a skip button method
    public void SkipCutscene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}