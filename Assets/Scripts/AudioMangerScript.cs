using System;
using UnityEngine;

public class AudioMangerScript : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;



    [Header("------- Audio Clip -------")]
    public AudioClip background;
    public AudioClip collision;
    public AudioClip gameOver;
    public AudioClip winning;

    private void Start()
    {
        musicSource.loop = true;
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    //---------------------------------------------
    public void PlayWinning()
    {
        if (SFXSource != null && winning != null)
            SFXSource.PlayOneShot(winning);
    }

    public void PlayCollision()
    {
      

        if (SFXSource != null && collision != null) {
            SFXSource.PlayOneShot(collision);
           
        }
            
    }
    //---------------------------------------------

    public void PlayGameOver()
    {
        if (SFXSource != null && gameOver != null)
            SFXSource.PlayOneShot(gameOver);
    }

    internal void PlaySFX(object v)
    {
        throw new NotImplementedException();
    }

    //--------------------stop-------------------------
    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }

}
