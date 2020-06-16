using UnityEngine;

public class Enemy : MonoBehaviour, IActivable
{ // Clase padre de todos los enemigos
    public bool pausa = true, active = true; // Los enemigos pueden pausarse o estar desactivados
    public EnemyManager enemyManager; // El manager de enemigos

    public virtual void SetPause()
    { // Funcion para poner la pausa segun la clase estatica
        pausa = StaticManager.pause;
    }

    public virtual void SetActivationState(bool activateState)
    { // Activar o desactivar el enemigo segun entrada
        active = activateState;
    }

    public void SetActivationState()
    { // Activar el enemigo (sin entrada se activa automaticamente)
        SetActivationState(true);
    }

    public void SetActivationState(int activateState)
    { // Activar enemigo segun entrada numerica, 0 desactivado sino activado
        if (activateState > 0)
        {
            SetActivationState(true);
        }
        SetActivationState(false);
    }

    public void SetEnemyManager(EnemyManager manager)
    { // Set de el enemyManager segun entrada
        enemyManager = manager;
    }

}
