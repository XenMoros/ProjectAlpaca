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

    public void SetActivationState()
    {
        SetActivationState(true);
    }

    public void SetActivationState(int activateState)
    {
        if (activateState > 0)
        {
            SetActivationState(true);
        }
        SetActivationState(false);
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager;
    }

}
