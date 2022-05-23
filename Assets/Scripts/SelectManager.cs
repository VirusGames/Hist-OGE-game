using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public GameObject toggleList;
    public Toggle[] allToggles;
    private bool canChange;
    public string states;
    public Scrollbar scroll;

    void Awake()
    {
            //For BOO thing ;)
        PlayerPrefs.SetString("Tags", "");
        scroll.value = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        allToggles = toggleList.transform.GetComponentsInChildren<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canChange) //Check for all toggles.
        {
            canChange = false;
            states = "";
            if(allToggles[0].isOn)
            {
                states+="Древняя Русь_";
            }
            if(allToggles[1].isOn)
            {
                states+="Золотая Орда_";
            }
            if(allToggles[2].isOn)
            {
                states+="Средневековье_";
            }
            if(allToggles[3].isOn)
            {
                states+="Смута_";
            }
            if(allToggles[4].isOn)
            {
                states+="Первые Романовы_";
            }
            if(allToggles[5].isOn)
            {
                states+="Дворцовые перевороты_";
            }
            if(allToggles[6].isOn)
            {
                states+="Александр I_";
            }
            if(allToggles[7].isOn)
            {
                states+="Николай I_";
            }
            if(allToggles[8].isOn)
            {
                states+="Александр II_";
            }
            if(allToggles[9].isOn)
            {
                states+="Александр III_";
            }
            if(allToggles[10].isOn)
            {
                states+="Николай II_";
            }
            if(states!="")
                states = states.Remove(states.Length - 1); //Remove last _
        }
    }

    public void ChangeState() //I'm sure it could be done better
    {
        canChange = true;
    }

    public void AllOn() //Turn all on
    {
        foreach(Toggle toggle in allToggles)
        {
            toggle.isOn = true;
        }
    }

    public void AllOff() //Turn all of
    {
        foreach(Toggle toggle in allToggles)
        {
            toggle.isOn = false;
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("Tags", states); //Save
    }
}