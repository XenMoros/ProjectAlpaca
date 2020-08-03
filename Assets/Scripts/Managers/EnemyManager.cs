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

        foreach (Enemy enemigo in encontrarEnemigos)
        {
            enemies.Add(enemigo);
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

    public void AlertarGuardias(Vector3 position)
    {
        foreach (Enemy enemigo in enemies)
        {
            if(enemigo.tipoEnemigo  == Enemy.TipoEnemigo.Guardia)
            {
                enemigo.AlertarEnemigo(position);
            }
            
        }
    }
}
