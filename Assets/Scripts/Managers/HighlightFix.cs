using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class HighlightFix : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IPointerUpHandler
{
    public void OnDeselect(BaseEventData eventData)
    {
        this.GetComponent<Selectable>().OnPointerExit(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!EventSystem.current.alreadySelecting && !Input.GetMouseButton(0))
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.isValid)
        {
            GameObject selectable;
            selectable = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent(typeof(Selectable)).gameObject;
           // if (eventData.pointerCurrentRaycast.gameObject.name == "Handle") selectable = eventData.pointerCurrentRaycast.gameObject.transform.parent.pa.gameObject;
           // else selectable = eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
            if (selectable != this.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(selectable);
            }
        }

       
    }
}
