using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IActivable
{
    public bool pausa = true, active = true;
    public EnemyManager enemyManager;

    public virtual void SetPause()
    {
        pausa = StaticManager.pause;
    }

    public virtual void SetActivationState(bool activateState)
    {
        active = activateState;
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager;
    }

}
