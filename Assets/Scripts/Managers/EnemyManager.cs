using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    LevelManager levelManager;

    public List<Enemy> enemies;

    internal void LoadEnemies()
    {
        Object[] encontrarEnemigos = FindObjectsOfType(typeof(Enemy));
        enemies = new List<Enemy>();

        foreach (Object enemigo in encontrarEnemigos)
        {
            enemies.Add((Enemy)enemigo);
        }

        foreach(Enemy enemigo in enemies)
        {
            enemigo.SetEnemyManager(this);
        }
    }

    public void SetLevelManager(LevelManager levelManager)
    {
        this.levelManager = levelManager;
    }

    public void SetPause()
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.SetPause();
        }
    }

    public void ReloadLevel()
    {
        levelManager.RestartLevel();
    }
}
