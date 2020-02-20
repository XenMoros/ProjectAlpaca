using UnityEngine;
using System.Collections;

public class SalidaNivel : MonoBehaviour
{

    LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.LevelComplete();
        }
    }

    public void SetLevelManager(LevelManager levelManager)
    {
        this.levelManager = levelManager;
    }
}
