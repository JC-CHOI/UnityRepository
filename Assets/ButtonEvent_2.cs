using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent_2 : MonoBehaviour
{
    bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GamePause()
    {
        if( isPause == false)
        {
            isPause = true;
            Time.timeScale = 0;

        }
        else
        {
            isPause = false;
            Time.timeScale = 1; // ¹è¼Ó
        }
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
