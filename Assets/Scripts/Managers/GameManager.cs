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

    int currentLevel, lastLevel, maxLevel;

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
        lastLevel = 1;
        currentLevel = 1;
        maxLevel = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1;
    }

    private void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCount > 1)
        {
            if (Input.GetButtonDown("Start"))
            {
                pause = !pause;
                levelManager.SetPause(pause);
                if (pause)
                {
                    interfaceManager.OpenPauseMenu();
                }
                else
                {
                    interfaceManager.CloseAllGroups();
                }
            }
        }
    }

    public void ContinueGame()
    {
        currentLevel = lastLevel;
        StartCoroutine(CargarEscena(currentLevel)); 
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

    IEnumerator CargarOtraEscena(int nivel)
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
        currentLevel = 1;
        StartCoroutine(CargarEscena(currentLevel));
    }

    public void Continue()
    {
        pause = false;
        levelManager.SetPause(pause);
    }

    public void RestartCurrentLevel()
    {
        StartCoroutine(CargarOtraEscena(currentLevel));
    }

    public void CargarSiguienteNivel()
    {
        interfaceManager.LoadingGroup(true);
        currentLevel++;
        lastLevel = Mathf.Min(Mathf.Max(lastLevel, currentLevel), maxLevel);
        
        if (currentLevel > maxLevel)
        {
            currentLevel = 1;
            StartCoroutine(DescargarEscenaActiva());
            interfaceManager.StartMainMenu();
        }
        else
        {
            StartCoroutine(CargarOtraEscena(currentLevel));
        }

    }

    public void ReturnToMain()
    {
        StartCoroutine(DescargarEscenaActiva());
    }

    public void LoadLevel(int nivel)
    {
        currentLevel = nivel;
        lastLevel = currentLevel;
        StartCoroutine(CargarEscena(nivel));
    }

}
