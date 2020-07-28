using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    InterfaceManager interfaceManager;
    internal LevelManager levelManager;
    internal CustomInputManager inputManager;
    internal AudioManager audioManager;
    //ScoreManager scoreManager;

    public LoadingSceneManager loadingSceneManager;

    public GameObject interfaceManagerPrefab,levelManagerPrefab,audioManagerPrefab;

    int currentLevel, lastLevel, maxLevel;

    internal void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        interfaceManager = Instantiate(interfaceManagerPrefab).GetComponent<InterfaceManager>();
        levelManager = Instantiate(levelManagerPrefab).GetComponent<LevelManager>();
        audioManager = Instantiate(audioManagerPrefab).GetComponent<AudioManager>();
        interfaceManager.Initialize();
        audioManager.Initialize();
        lastLevel = 1;
        currentLevel = 1;
        maxLevel = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1;
        inputManager = GetComponent<CustomInputManager>();
    }

    private void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCount > 1)
        {
            if (inputManager.GetButtonDown("Start"))
            {
                StaticManager.pause = !StaticManager.pause;
                levelManager.SetPause();
                if (StaticManager.pause)
                {
                    interfaceManager.OpenPauseMenu();
                    StaticManager.ChangeSensibility(0);
                }
                else
                {
                    interfaceManager.CloseAllGroups();
                    StaticManager.ChangeSensibility(StaticManager.lastSensibility);
                }
            }
        }
    }

    public void ContinueGame()
    {// Continue Game button in main menu
        currentLevel = lastLevel;
        StartCoroutine(CargarEscena(currentLevel)); 
    }

    IEnumerator CargarEscena(int nivel)
    {
        float tiempoCarga = 0f;
        UnityEngine.SceneManagement.Scene escena = levelManager.LoadLevel(nivel);
        loadingSceneManager.LoadLoadingAnimation();

        while (!escena.isLoaded || tiempoCarga<5f)
        {
            tiempoCarga += Time.deltaTime;
            yield return null;
        }

        StaticManager.pause = false;
        levelManager.SetPause(true);
        interfaceManager.LoadingGroup(false);
        loadingSceneManager.UnloadLoadingAnimation();

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

            yield return null;
        }

        StartCoroutine(CargarEscena(nivel));

    }
    public void NewGame()
    {// New Game button in main menu
        currentLevel = 1;
        StartCoroutine(CargarEscena(currentLevel));
    }

    public void Continue()
    {// Continue button in pause menu
        StaticManager.pause = false;
        StaticManager.ChangeSensibility(StaticManager.lastSensibility);
        levelManager.SetPause();
    }

    public void RestartCurrentLevel()
    {// Restart level in pause menu
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
