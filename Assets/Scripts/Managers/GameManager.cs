using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    InterfaceManager interfaceManager;
    LevelManager levelManager;
    //AudioManager audioManager;
    //ScoreManager scoreManager;

    public GameObject interfaceManagerPrefab,levelManagerPrefab;

    int ultimoNivel = 1;
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
        interfaceManager.Initialize();
    }

    private void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCount > 1)
        {
            if (Input.GetButtonDown("Start") && pause == false)
            {
                pause = true;
                levelManager.SetPause(pause);
                interfaceManager.OpenPauseMenu();
            }
            else if (Input.GetButtonDown("Start"))
            {
                pause = false;
                levelManager.SetPause(pause);
                interfaceManager.ClosePauseMenu();
            }
        }
    }

    public void ContinueGame()
    {
        StartCoroutine(CargarEscena(ultimoNivel)); 
    }

    IEnumerator CargarEscena(int nivel)
    {
        UnityEngine.SceneManagement.Scene escena = levelManager.LoadLevel(nivel);

        while (!escena.isLoaded)
        {
            yield return null;
        }

        pause = false;
        levelManager.SetPause(pause);
        interfaceManager.LoadingGroup(false);
    }

    IEnumerator DescargarEscenaActiva()
    {
        UnityEngine.SceneManagement.Scene escena = levelManager.UnloadLevel();

        while(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != escena.name)
        {
            yield return null;
        }
    }

    IEnumerator RescargarEscenaActiva(int nivel)
    {
        UnityEngine.SceneManagement.Scene escena = levelManager.UnloadLevel();

        while (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != escena.name)
        {
            Debug.Log("Descargandooo");
            yield return null;
        }

        StartCoroutine(CargarEscena(nivel));

    }
    public void NewGame()
    {
        StartCoroutine(CargarEscena(1));
    }

    public void Continue()
    {
        pause = false;
        levelManager.SetPause(pause);
        interfaceManager.ClosePauseMenu();
    }

    public void RestartCurrentLevel()
    {

        StartCoroutine(RescargarEscenaActiva(1));


    }

    public void ReturnToMain()
    {

        StartCoroutine(DescargarEscenaActiva());

    }
}
