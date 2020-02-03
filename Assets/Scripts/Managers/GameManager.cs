using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    InterfaceManager interfaceManager;
    LevelManager levelManager;
    //AudioManager audioManager;
    //ScoreManager scoreManager;

    public GameObject interfaceManagerPrefab,levelManagerPrefab;

    bool pause = true;

    //ScoreManager scoreManager;

    internal void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        interfaceManager = Instantiate(interfaceManagerPrefab).GetComponent<InterfaceManager>();
        levelManager = Instantiate(levelManagerPrefab).GetComponent<LevelManager>();
        interfaceManager.StartMainMenu();
    }

    private void Update()
    {

        if (Input.GetButtonDown("Start") && pause==false)
        {
            pause = true;
            levelManager.SetPause(pause);
            interfaceManager.OpenPauseMenu();
        }
        else if (Input.GetButtonDown("Start"))
        {
            pause = false;
            levelManager.SetPause(pause);
        }
    }

    public void ContinueGame()
    {
        pause = false;
        levelManager.LoadLevel(0);
        levelManager.SetPause(pause);
    }

    public void NewGame()
    {
        pause = false;
        levelManager.LoadLevel(0);
        levelManager.SetPause(pause);
    }

    public void Continue()
    {
        pause = false;
        levelManager.SetPause(pause);
    }

    public void Restart()
    {
        interfaceManager.StartMainMenu();
        pause = false;
        levelManager.SetPause(pause);
    }
}
