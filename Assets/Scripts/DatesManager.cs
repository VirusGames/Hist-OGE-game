using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class DatesManager : MonoBehaviour
{
        //import things
    public MenuManager menuManager;
    public Music music;
    public TextAsset textJSON;
    private StatesList loadedGameData;
    public List<StatesJSON> Dates;
    private List<int> deleteList;
    public bool inEditor;
    public string[] Tags;
    private List<string> former;
    private List<int> lst;
    public int answer;
    public int wins = 0;
    public int loses = 0;
    //private ListExtensions piss = new ListExtensions;

        //import sounds
    [Header("Sounds")]
    public AudioSource source;
    public AudioClip good;
    public AudioClip bad;
    public AudioClip spook;

        //UI things
    [Header("UI")]
    public Text Title;
    public GameObject buttonsList;
    public Text[] buttons;
    public Text score;
    public Image face;
    public GameObject folder;
    public Text endTitle;
    public Text endText;
    public GameObject resetFolder;
    public GameObject Fred;

    private void Awake()
    {
        if(inEditor) //necessary for tests 
        {
            Tags = new string[]{"Первые Романовы"};
        }
        else //For actual game
        {
            if(PlayerPrefs.GetString("Tags", "") == "")
            {
                Tags = new string[]{""};
            }
            else
            {
                Tags = PlayerPrefs.GetString("Tags", "").Split(char.Parse("_"));
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(Tags[0]!="") //If tags selected
        {
            //wins++;
            resetFolder.SetActive(false);
            Color c = face.color;
            c.a = 0.5f;
            face.color = c;
            buttons = buttonsList.transform.GetComponentsInChildren<Text>();
            LoadDataNonPath(textJSON.text);
            MainTask();
        }
        else //if not ;)
        {
            StartCoroutine(BOO());
        }
    }

    private IEnumerator BOO() //BOO!
    {
        //source.volume = menuManager.soundsVolume;
        float temp = menuManager.musicSlider.value;
        menuManager.musicSlider.value = 0;
        source.PlayOneShot(spook);
        Fred.SetActive(true);
        yield return new WaitForSeconds(3f);
        //menuManager.musicSlider.value = temp;
        Application.Quit();
    }

    public void LoadDataNonPath(string text)
    { 
            //Load data from JSON
        loadedGameData = JsonUtility.FromJson<StatesList>(text);
        Dates = loadedGameData.states.ToList();
        bool fuck;
        int deleted = 0;;
        deleteList = new List<int> {};
        for(int i = 0; i < Dates.Count; i++) //Delete unnecessary things 
        {
            fuck = true;
            for(int j = 0; j < Tags.Length; j++)
            {
                if(Dates[i].tags.Contains(Tags[j]))
                {
                    fuck = false;
                }
            }
            if(fuck)
            {
                deleteList.Add(i);
            }
        }
        foreach(int num in deleteList)
        {
            Dates.Remove(Dates[num-deleted]);
            deleted++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = menuManager.soundsVolume;
    }

    private void MainTask()
    {
        folder.SetActive(false);
        score.text = wins.ToString()+"/"+loses.ToString();
        int num = UnityEngine.Random.Range(0, Dates.Count-1);
        Title.text = Dates[num].statement;
        //lst = new List<int>{int.Parse(Dates[num].date.Substring(0, 4))};
        lst = new List<int>{num};
        //BitchList former = new BitchList();
        //List<Bitch> formerer = former.list.ToList();
        former = new List<string>{Dates[num].date};
        //List<string> tetet = new List<string> {Dates[lst[1]].date, Dates[0].date};
        int rnd=num;
        bool same = true;
        int x = 5; int y = 0;
            //It's piece of shit, not the loop
        for(int i = 0; i<3; i++)
        {
            do{
                if(y>=20) //To prevent endless looping
                    x++;
                rnd = UnityEngine.Random.Range(num-x, num+x);
                if(rnd>=0&&rnd<Dates.Count)
                {
                    /*for(int j = 0; j <lst.Count; j++)
                    {
                        former
                        if(Dates[lst[j]].date==Dates[rnd].date)
                        {
                            same = true;
                            break;
                        }
                        same = false;
                    }*/
                    if(former.Contains(Dates[rnd].date)||lst.Contains(rnd)) // If we already have this date, pick another
                    {
                        same = true;
                        y++;
                    }
                    else
                    {
                        same = false;
                    }
                }
                else
                    same = true;
                
            }while(same);
            lst.Add(rnd);
            former.Add(Dates[rnd].date);
        }
        lst = Shuffle<int>(lst); //shuffle list a bit
        for (int i=0; i<lst.Count; i++) {
            if (lst[i] == num) {
                answer = i; //right answer
                break;
            }
        }
        for (int i=0; i<4; i++)
        {
            buttons[i].text = Dates[lst[i]].date;
        }
    }

        //These 2 functions was taken from the Internet
    public List<T> Shuffle<T>(List<T> list) {
        System.Random rnd = new System.Random();
        for (var i = 0; i < list.Count; i++)
            list = Swap(list, i, rnd.Next(i, list.Count));
        return list;
    }
 
    public List<T> Swap<T>(List<T> list, int i, int j) {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
        return list;
    }

        //Simple func
    public void Check(int str)
    {
        if(str==answer)
        {
            Win();
        }
        else
        {
            Lose();
        }
    }

    private void Win() //Wow, you exist
    {
        wins++;
        source.PlayOneShot(good);
        folder.SetActive(true);
        endTitle.text = "Правильно!";
        endText.text = Dates[lst[answer]].date + " - " + Dates[lst[answer]].statement;
    }

    private void Lose() //I GET ANGRIER FOR EVERY PROBLEM YOU GET WRONG
    {
        loses++;
        source.PlayOneShot(bad);
        folder.SetActive(true);
        endTitle.text = "Неправильно!";
        endText.text = Dates[lst[answer]].date + " - " + Dates[lst[answer]].statement;
    }

    public void Next() //Next question
    {
        source.Stop();
        MainTask();
    }

    public void AgainMenu() //return to menu?
    {
        music.musicSource.Pause();
        resetFolder.SetActive(true);
    }

    public void Again() //return to menu
    {
        SceneManager.LoadScene("Start");
    }

    public void NotAgain() //not return to menu
    {
        music.musicSource.Play();
        resetFolder.SetActive(false);
    }

        //JSON thing
    [System.Serializable]
    public class StatesJSON
    {
        public string date;
        public string statement;
        public string[] tags;
    }

    [System.Serializable]
    public class StatesList
    {
        public StatesJSON[] states;
    }

        //unused bitches
    [System.Serializable]
    public class Bitch
    {
        public List<string> list;
        public Bitch()
        {
            list = new List<string>();
        }
    }
    
    [System.Serializable]
    public class BitchList
    {
        public List<Bitch> list;
    }
}