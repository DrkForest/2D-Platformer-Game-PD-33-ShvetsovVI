using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class GameManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayerPrefs.SetInt(GamePrefs.LastPlayedLvl.ToString(), SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt(GamePrefs.LvlPlayed.ToString() + SceneManager.GetActiveScene().buildIndex, 1);

        }
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static GameManager Instanse;

    private void Awake()
    {
        if (Instanse == null)
        {
            Instanse = this;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
    public void ChangeLevel(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}

public enum Scenes
{
    MainMenu,
    first,
    second, 
    third,
}

public enum GamePrefs
{
    LastPlayedLvl,
    LvlPlayed
}