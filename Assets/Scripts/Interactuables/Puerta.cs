using UnityEngine;

public class Puerta : MonoBehaviour, IActivable
{
    public BoxCollider puerta1;
    public BoxCollider puerta2;
    public BoxCollider puerta3;

    public Animator puertaAnimator;

    // En caso de activacion, cambiar el estado de enable del renderizado y el collider de la puerta
    public void SetActivationState(bool activateState)
    {
        if (activateState)
        {
            puertaAnimator.SetBool("puertaAbierta", true);

            puerta1.enabled = false;
            puerta2.enabled = false;
            puerta3.enabled = false;
        }
        else
        {
            puertaAnimator.SetBool("puertaAbierta", false);

            puerta1.enabled = true;
            puerta2.enabled = true;
            puerta3.enabled = true;
        }
    }
}
