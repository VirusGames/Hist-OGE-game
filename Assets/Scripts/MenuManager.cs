using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] string nextLevelName;
    public float musicVolume;
    public float soundsVolume;
    public Slider musicSlider;
    public Slider soundsSlider;
    public Image face;
    public GameObject folder;
    public Text testText;
    //public Button next;
    
    // Start is called before the first frame update

    void Awake()
    {
            //Load data
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1);
        soundsSlider.value = PlayerPrefs.GetFloat("soundsVolume", 1);
    }
    void Start()
    {
            //Set Background alpha and disable
        Color c = face.color;
        c.a = 0.5f;
        face.color = c;
        //face.enabled = false;
        folder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        musicVolume = musicSlider.value;
        //testText.text = musicVolume.ToString();
        soundsVolume = soundsSlider.value;
    }

    public void Next() //Next
    {
        SceneManager.LoadScene(nextLevelName);
    }

    public void EnterMenu()
    {
        //face.enabled = true;
        folder.SetActive(true);
    }
    public void ExitMenu()
    {
            //Load save save data
        //face.enabled = false;
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("soundsVolume", soundsSlider.value);
        PlayerPrefs.Save();
        folder.SetActive(false);
    }

}
