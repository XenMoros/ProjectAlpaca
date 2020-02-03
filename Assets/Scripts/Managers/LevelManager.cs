using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    GameManager gameManager;
    EnemyManager enemyManager;
    AlpacaMovement alpaca;

    GameObject currentLevel;

    public GameObject enemyManagerPrefab;
    public List<GameObject> levelsPrefabs;

    private void Start()

    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyManager = Instantiate(enemyManagerPrefab).GetComponent<EnemyManager>();

    }

    public void SetPause(bool state)
    {
        alpaca.SetPause(state);
        enemyManager.SetPause(state);
    }

    public void LoadLevel(int nivel)
    {
        currentLevel = Instantiate(levelsPrefabs[0]);
        alpaca = GameObject.Find("Alpaca").GetComponent<AlpacaMovement>();
        enemyManager.LoadEnemies();
    }

    public void UnloadLevel()
    {
        Destroy(currentLevel);
    }

    public void RestartLevel()
    {
        UnloadLevel();
        LoadLevel(0);
    }

}
