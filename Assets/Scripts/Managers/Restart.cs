using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour, IActivable
{

    Scene currentScene; // Escena actual

    private void Start()
    {
        // Captura escena actual
        currentScene = SceneManager.GetActiveScene();
    }

    // En activacion, recargar la escena actual
    public void SetActivationState(bool activateState)
    {
        if (activateState)
        {
            SceneManager.LoadScene(currentScene.name);
        }
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
}
