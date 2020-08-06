using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AutoScroll : MonoBehaviour
{

    RectTransform scrollRectTransform;
    RectTransform contentPanel;
    RectTransform selectedRectTransform;
    GameObject lastSelected;
    Vector3[] selectedWorldPosition = new Vector3[4];
    Vector3[] viewWorldPosition = new Vector3[4];

    void Start()
    {
        scrollRectTransform = GetComponent<RectTransform>();
        contentPanel = GetComponent<ScrollRect>().content;
    }

    void Update()
    {
        // Get the currently selected UI element from the event system.
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        
        // Return if there are none.
        if (selected == null)
        {
            return;
        }
        // Return if the selected game object is not inside the scroll rect.
        if (selected.transform.parent != contentPanel.transform)
        {
            return;
        }
        // Return if the selected game object is the same as it was last frame,
        // meaning we haven't moved.
        if (selected == lastSelected)
        {
            return;
        }

        // Get the rect tranform for the selected game object.
        selectedRectTransform = selected.GetComponent<RectTransform>();

        selectedRectTransform.GetWorldCorners(selectedWorldPosition); 
        scrollRectTransform.GetWorldCorners(viewWorldPosition); 

        if (selectedWorldPosition[3].y < viewWorldPosition[3].y)
        {
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, contentPanel.anchoredPosition.y + (viewWorldPosition[3].y- selectedWorldPosition[3].y));
        }
        else if (selectedWorldPosition[2].y > viewWorldPosition[2].y)
        {
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, contentPanel.anchoredPosition.y + (viewWorldPosition[2].y- selectedWorldPosition[2].y));
        }

        lastSelected = selected;
    }
}