using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    internal InterfaceManager interfaceManager;
    internal LevelManager levelManager;
    internal CustomInputManager inputManager;
    internal MusicAudioManager audioManager;

    public LoadingSceneManager loadingSceneManager;

    public GameObject interfaceManagerPrefab,levelManagerPrefab,audioManagerPrefab;

    bool canPause = false;
    int currentLevel, lastLevel, maxLevel;

    internal void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        interfaceManager = Instantiate(interfaceManagerPrefab).GetComponent<InterfaceManager>();
        levelManager = Instantiate(levelManagerPrefab).GetComponent<LevelManager>();
        audioManager = Instantiate(audioManagerPrefab).GetComponent<MusicAudioManager>();
        inputManager = GetComponent<CustomInputManager>();
        interfaceManager.Initialize();
        loadingSceneManager.LoadAlpaca();
        audioManager.GameAudio(0);
        lastLevel = 1;
        currentLevel = 1;
        maxLevel = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1; 
    }

    private void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCount > 1 && canPause)
        {
            if (inputManager.GetButtonDown("Start") &&
                !(levelManager.alpaca.faseMovimiento == AlpacaMovement.FaseMovimiento.Stopped &&
                levelManager.alpaca.tipoStopped == AlpacaMovement.TipoStopped.Cinematica))
            {
                StaticManager.SetPause(!StaticManager.pause);
                if (StaticManager.pause)
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
    {// Continue Game button in main menu
        currentLevel = lastLevel;
        StartCoroutine(CargarEscena(currentLevel)); 
    }

    IEnumerator CargarEscena(int nivel)
    {
        audioManager.StopAudio();
        float tiempoCarga = 0f;
        canPause = false;
        StaticManager.SetPause(true);

        loadingSceneManager.LoadLoadingAnimation();
        loadingSceneManager.UnloadAlpaca();
        UnityEngine.SceneManagement.Scene escena = levelManager.LoadLevel(nivel);

        while (!escena.isLoaded || tiempoCarga<5f)
        {
            tiempoCarga += Time.deltaTime;
            yield return null;
        }

        levelManager.SetCarga();
        canPause = true;
        interfaceManager.LoadingGroup(false);
        loadingSceneManager.UnloadLoadingAnimation();
        audioManager.GameAudio(nivel);

    }

    IEnumerator DescargarEscenaActiva()
    {
        audioManager.StopAudio();
        float tiempoCarga = 0f;
        canPause = false;
        loadingSceneManager.LoadLoadingAnimation();
        UnityEngine.SceneManagement.Scene escena = levelManager.UnloadLevel();

        while(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != escena.name || tiempoCarga < 5f)
        {
            tiempoCarga += Time.deltaTime;
            yield return null;
            
        }

        interfaceManager.LoadingGroup(false);
        loadingSceneManager.UnloadLoadingAnimation();
        interfaceManager.StartMainMenu();
        loadingSceneManager.LoadAlpaca();
        audioManager.GameAudio(0);
    }

    IEnumerator CargarOtraEscena(int nivel)
    {
        audioManager.StopAudio();
        canPause = false;
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
        StaticManager.SetPause(false);
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
