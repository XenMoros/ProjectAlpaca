using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreventDeselectUI : MonoBehaviour
{
    EventSystem evt;
    GameObject lastSelect;
    Coroutine preventDeselect;
    private void Start()
    {
        evt = EventSystem.current;
        StaticManager.OnPauseChange += SetDeselectPrevention;
        preventDeselect = StartCoroutine(PreventDeselect());
    }

    private void OnDisable()
    {
        StaticManager.OnPauseChange -= SetDeselectPrevention;
    }

    void SetDeselectPrevention()
    {
        if (StaticManager.pause)
        {
            preventDeselect = StartCoroutine(PreventDeselect());
        }
        else
        {
            StopCoroutine(preventDeselect);
        }
    }


    IEnumerator PreventDeselect()
    {
        while (true)
        {
            if (evt.currentSelectedGameObject != null && evt.currentSelectedGameObject != lastSelect)
            {
                lastSelect = evt.currentSelectedGameObject;
            }
            else if (lastSelect != null && evt.currentSelectedGameObject == null)
            {
                evt.SetSelectedGameObject(lastSelect);
            }
            yield return null;
        }
    }
}
