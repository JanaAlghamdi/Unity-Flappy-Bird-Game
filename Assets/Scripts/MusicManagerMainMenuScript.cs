using UnityEngine;

public class MusicManagerMainMenuScript: MonoBehaviour
{
    public static MusicManagerMainMenuScript Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float fadeOutSeconds = 0.8f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicSource != null && !musicSource.isPlaying)
            musicSource.Play();
    }

    public void StopMenuMusic()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(FadeOutAndStop());
    }

    private System.Collections.IEnumerator FadeOutAndStop()
    {
        if (musicSource == null) yield break;

        float startVol = musicSource.volume;
        float t = 0f;
        while (t < fadeOutSeconds)
        {
            t += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(startVol, 0f, t / fadeOutSeconds);
            yield return null;
        }
        musicSource.Stop();
        musicSource.volume = startVol; 
    }
    public void StartMenuMusicIfNeeded(bool forceRestart = false)
    {
        if (musicSource == null) return;

        if (forceRestart)
        {
            
            musicSource.Stop();
            musicSource.Play();
        }
        else if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

}

