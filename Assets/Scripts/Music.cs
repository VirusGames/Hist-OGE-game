using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //This script is short
public class Music : MonoBehaviour
{
    public AudioSource musicSource;
	public AudioClip musicStart;
    public MenuManager menuManager;
    public bool loop;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.volume = menuManager.musicVolume;
        if(loop) //If track has unlooping beginning
        {
            musicSource.PlayOneShot(musicStart);
		    musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        musicSource.volume = menuManager.musicVolume;
    }
}
