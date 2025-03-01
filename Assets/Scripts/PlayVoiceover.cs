using UnityEngine;

public class PlayVoiceover : MonoBehaviour
{
    public AudioSource voiceoverAudio;

    void Start()
    {
        voiceoverAudio.Play();
    }
}
