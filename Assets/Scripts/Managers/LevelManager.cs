using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    GameManager gameManager;
    EnemyManager enemyManager;
    public AlpacaMovement alpaca;

    public GameObject enemyManagerPrefab;

    private AsyncOperation sceneAsync;
    private Scene escenaMenus,escenaNivel;

    bool levelComplete = false;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyManager = Instantiate(enemyManagerPrefab).GetComponent<EnemyManager>();
        escenaMenus = SceneManager.GetActiveScene();
        enemyManager.SetLevelManager(this);
        Application.backgroundLoadingPriority = ThreadPriority.Low;
    }

    public void SetCarga()
    {

            alpaca.EntradaNivel();
    }

    public Scene LoadLevel(int nivel)
    {

        StartCoroutine(LoadScene(nivel));
        escenaNivel = SceneManager.GetSceneByBuildIndex(nivel);

        levelComplete = false;

        return (escenaNivel);
    }

    public IEnumerator LoadScene(int nivel)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(nivel, LoadSceneMode.Additive);
        scene.allowSceneActivation = false;
        sceneAsync = scene;

        do
        {
            Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
            yield return null;
        } while (scene.progress < 0.9f);

        OnFinishedLoadingAllScene(nivel);
    }

    private void OnFinishedLoadingAllScene(int nivel)
    {
        StartCoroutine(EnableScene(nivel));
    }

    IEnumerator EnableScene(int nivel)
    {
        //Activate the Scene
        sceneAsync.allowSceneActivation = true;

        while (!sceneAsync.isDone)
        {
            yield return null;
        }

        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(nivel);
        if (sceneToLoad.IsValid())
        {
            Debug.Log("Scene is Valid");
            SceneManager.SetActiveScene(sceneToLoad);
            
            foreach(GameObject objeto in sceneToLoad.GetRootGameObjects())
            {
                if(objeto.name == "Alpaca")
                {
                    alpaca = objeto.GetComponent<AlpacaMovement>();
                    yield return null;
                    alpaca.SetInputManager(gameManager.inputManager);
                    yield return null;
                    objeto.GetComponent<AlpacaAudioManager>().SetManagers(gameManager.inputManager, this);
                    yield return null;
                    objeto.GetComponent<CozScript>().SetInputManager(gameManager.inputManager);
                    yield return null;
                    objeto.GetComponent<EscupitajoAction>().SetInputManager(gameManager.inputManager);
                    yield return null;
                    objeto.GetComponent<InteractScript>().SetInputManager(gameManager.inputManager);
                    yield return null;
                }
                
                if(objeto.name == "Entorno")
                {
                    objeto.GetComponentInChildren<SalidaNivel>().SetLevelManager(this);
                    yield return null;
                }
            }
            enemyManager.LoadEnemies();
        }
    }

    public void AlertarGuardias(Vector3 position)
    {
        enemyManager.AlertarGuardias(position);
    }

    public Scene UnloadLevel()
    {
        StartCoroutine(UnloadScene());

        return escenaMenus;
    }

    IEnumerator UnloadScene()
    {
        //SceneManager.SetActiveScene(escenaMenus);

        AsyncOperation scene = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        while (!scene.isDone)
        {
            yield return null;
        }

    }

    public void RestartLevel()
    {
        gameManager.RestartCurrentLevel();
    }

    public IEnumerator LevelComplete()
    {
        alpaca.SalidaNivel();

        while (!levelComplete)
        {
            levelComplete = alpaca.exitReached;
            yield return null;
        }

        gameManager.CargarSiguienteNivel();
    }

}
