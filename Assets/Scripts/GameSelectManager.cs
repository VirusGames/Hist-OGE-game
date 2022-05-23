using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    //This script is short
public class GameSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DatesNext()
    {
        SceneManager.LoadScene("DatesGame");
    }

    public void StatementsNext()
    {
        SceneManager.LoadScene("StatementsGame");
    }
}
