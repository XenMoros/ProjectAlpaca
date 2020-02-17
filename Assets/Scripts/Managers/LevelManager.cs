using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    GameManager gameManager;
    EnemyManager enemyManager;
    public AlpacaMovement alpaca;

    GameObject currentLevel;

    public GameObject enemyManagerPrefab;
    public List<GameObject> levelsPrefabs;

    private AsyncOperation sceneAsync;
    private Scene escena;



    private void Start()

    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyManager = Instantiate(enemyManagerPrefab).GetComponent<EnemyManager>();

    }

    public void SetPause(bool state)
    {
        alpaca.SetPause(state);
        //enemyManager.SetPause(state);
    }



    public Scene LoadLevel(int nivel)
    {

        StartCoroutine(LoadScene(nivel));
        escena = SceneManager.GetSceneByBuildIndex(nivel);
        return (escena);
        //alpaca = GameObject.Find("Alpaca").GetComponent<AlpacaMovement>();
        //enemyManager.LoadEnemies();
    }

    public IEnumerator LoadScene(int nivel)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(nivel, LoadSceneMode.Additive);
        scene.allowSceneActivation = false;
        sceneAsync = scene;

        while (scene.progress < 0.9f)
        {
            Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
            yield return null;
        }

        OnFinishedLoadingAllScene(nivel);
        
    }

    private void OnFinishedLoadingAllScene(int nivel)
    {
        StartCoroutine(enableScene(nivel));
    }

    IEnumerator enableScene(int nivel)
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
                    Debug.Log("Encontre la Alpaca");
                    alpaca = objeto.GetComponent<AlpacaMovement>();
                }
            }
            
        }
    }

    public void UnloadLevel()
    {
        Destroy(currentLevel);
    }

    public void RestartLevel()
    {
        UnloadLevel();
        LoadLevel(1);
    }

}
