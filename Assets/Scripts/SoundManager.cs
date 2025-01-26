using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;

    public AudioClip pickUpSfx;
    public AudioClip jumpSfx;
    public AudioClip UIButtonSfx;
    public AudioClip dieClip;



    private void Awake()
    {
        // Ensure there is only one instance of SoundManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Play background music at the start, if assigned
        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic);
        }
    }

    public void PlayDieClip()
    {
        PlaySFX(dieClip);
    }


    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }


    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayButtonSfx()
    {
        PlaySFX(UIButtonSfx);
    }

    public void PlayPickUpSfx()
    {
        PlaySFX(pickUpSfx);
    }

    public void PlayJumpSfx()
    {
        PlaySFX(jumpSfx);
    }


    public void StopMusic()
    {
        musicSource.Stop();
    }
}
