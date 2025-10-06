using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource bossmusicSource;

    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip bossfight;
    public AudioClip punch;
    public AudioClip dialogue;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
