using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip cursor;
    public AudioClip drop;
    public AudioClip control;
    public AudioClip lineclean;
    private AudioSource audioSource;
    
    

    private bool isMute = false;

    private void Awake()
    {
        audioSource=GetComponent<AudioSource>();
    }
    public void PlayCursor()
    {
        PlayAudio(cursor);
    
    
    }


    public void PlayContorl()
    {

        PlayAudio(control);


    }
    public void PlayLineClean()
    {

        PlayAudio(lineclean);

    }



    public void PlayDrop()
    {
        PlayAudio(drop);
    }
    public void PlayAudio(AudioClip clip)
    {
        if (isMute) return;
        audioSource.clip = clip;
        audioSource.Play();
    
    }
    

}
