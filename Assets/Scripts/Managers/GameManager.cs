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

        StartCoroutine(CargarEscena(1));
        

    }

    IEnumerator CargarEscena(int nivel)
    {
        UnityEngine.SceneManagement.Scene escena = levelManager.LoadLevel(nivel);

        while (!escena.isLoaded)
        {
            yield return null;
        }

        levelManager.SetPause(pause);
        interfaceManager.CloseAllGroups();
    }

    public void NewGame()
    {
        pause = false;
        StartCoroutine(CargarEscena(1));
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
