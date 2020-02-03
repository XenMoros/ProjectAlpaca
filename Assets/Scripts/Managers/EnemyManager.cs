using UnityEngine;
using System.Collections;
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
    }

    private void LateStart()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    public void SetPause(bool state)
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.SetPause(state);
        }
    }
}
