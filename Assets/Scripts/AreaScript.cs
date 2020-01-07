using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AreaScript : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook camara;
    public bool isDoor = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isDoor)
            {
                camara.Priority = 12;
            }
            else
            {
                camara.Priority = 11;
            }
            camara.MoveToTopOfPrioritySubqueue();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camara.Priority = 10;
        }
    }
}
