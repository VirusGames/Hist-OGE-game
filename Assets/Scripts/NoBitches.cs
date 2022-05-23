using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

    //No bitches(
public class NoBitches : MonoBehaviour
{
    public AudioSource music;
    public GameObject lenny;
    // Start is called before the first frame update
    private void Awake()
    {
        lenny.SetActive(false);
    }
    void Start()
    {
        StartCoroutine(Bitch());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Bitch()
    {
        yield return new WaitWhile(() => !music.isPlaying);
        yield return new WaitForSeconds(1.89f);
        lenny.SetActive(true);
        yield return new WaitWhile(() => music.isPlaying);
        //yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Start");
    }
}
