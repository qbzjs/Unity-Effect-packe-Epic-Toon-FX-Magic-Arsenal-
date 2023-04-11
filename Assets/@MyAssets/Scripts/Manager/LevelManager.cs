using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject failScreen;
    public List<GameObject> Levels;
    public int levelIndex;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        winScreen.Hide();
        failScreen.Hide();
        LoadData();
    }

    void Start()
    {

    }

    public void LoadData()
    {
        levelIndex = PlayerPrefs.GetInt("LevelIndex", 1);
        if (levelIndex >= Levels.Count)
        {
            levelIndex = 0;
        }
        /*else
        {
            levelIndex = count;
        }*/
        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].gameObject.Hide();
        }
        Levels[levelIndex].gameObject.Show();
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("LevelIndex", levelIndex + 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void FailLevel()
    {
        failScreen.Show();
    }
    public void WinLevel()
    {
        winScreen.Show();
    }
}
