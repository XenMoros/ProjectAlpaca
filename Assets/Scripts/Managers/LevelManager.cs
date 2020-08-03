﻿using UnityEngine;
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
    }

    public void SetPause(bool carga = false)
    {

        alpaca.SetPause();
        enemyManager.SetPause();

        if (carga)
        {
            alpaca.EntradaNivel();
        }
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

        while (scene.progress < 0.9f)
        {
            Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
            yield return null;
        }

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
                    alpaca.SetInputManager(gameManager.inputManager);
                    objeto.GetComponent<AlpacaSound>().SetInputManager(gameManager.inputManager);
                    objeto.GetComponent<AlpacaSound>().SetManagers(gameManager.audioManager,this);
                    objeto.GetComponent<CozScript>().SetInputManager(gameManager.inputManager);
                    objeto.GetComponent<EscupitajoAction>().SetInputManager(gameManager.inputManager);
                    objeto.GetComponent<InteractScript>().SetInputManager(gameManager.inputManager);
                }
                
                if(objeto.name == "Entorno")
                {
                    objeto.GetComponentInChildren<SalidaNivel>().SetLevelManager(this);
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
