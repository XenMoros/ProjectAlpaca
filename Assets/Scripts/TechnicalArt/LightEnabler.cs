using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class LightEnabler : MonoBehaviour
{
    public List<GameObject> luces;
    public bool isActivationZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject luz in luces)
            {
                if (luz.activeSelf != isActivationZone)
                {
                    luz.SetActive(isActivationZone);
                }
            }
        }
    }
}
