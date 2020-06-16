using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DinamicLevels : MonoBehaviour
{

    Resolution res;
    public InterfaceManager interfaceManager;
    public List<Button> niveles = new List<Button>();
    public Button prefabButton;
    public Transform content;
    public Button returnButton;
    public int numNiveles;
    public float porcionVertical = 1;

    RectTransform contentRT;


    void Start()
    {
        res = Screen.currentResolution;
        Debug.Log(res);
        //numNiveles = EditorBuildSettings.scenes.Length - 1;
        numNiveles = 5;
        contentRT = content.GetComponent<RectTransform>();

        contentRT.anchorMin = new Vector2(0, 1);
        contentRT.anchorMax = new Vector2(1, 1);

        //contentRT.offsetMin = new Vector2(0, numNiveles * -75);
        contentRT.offsetMin = new Vector2(0, - numNiveles * (res.height/ porcionVertical));

        Navigation botonN = new Navigation();
        botonN.mode = Navigation.Mode.Explicit;

        for (int i = 0; i < numNiveles; i++)
        {
            niveles.Add(Instantiate(prefabButton, content.position, Quaternion.identity, content));

            RectTransform botonRT = niveles[i].GetComponent<RectTransform>();
            int j = i;

            botonRT.anchorMin = new Vector2(0, 1-((float)j+1)/(float)numNiveles);
            botonRT.anchorMax = new Vector2(1, 1-(float)j/(float)numNiveles);

            botonRT.offsetMax = Vector2.zero;
            botonRT.offsetMin = Vector2.zero;
            
            niveles[i].onClick.AddListener(() => interfaceManager.LevelButton(j + 1));

            niveles[i].GetComponentInChildren<TextMeshProUGUI>().text = "Level " + (i + 1);
        }

        interfaceManager.selectDefecto[3] = niveles[0];

        for (int i = 0; i < numNiveles; i++)
        {
            int j = i;

            if (i == 0)
            {
                botonN.selectOnDown = niveles[j + 1];
                botonN.selectOnUp = returnButton;
            }
            else if (i == numNiveles - 1)
            {
                botonN.selectOnDown = returnButton;
                botonN.selectOnUp = niveles[j - 1];
            }
            else
            {
                botonN.selectOnDown = niveles[j + 1];
                botonN.selectOnUp = niveles[j - 1];
            }

            niveles[i].navigation = botonN;
        }
        botonN.selectOnUp = niveles[numNiveles - 1];
        botonN.selectOnDown = niveles[0];

        returnButton.navigation = botonN;
    }

    private void Update()
    {
        if (res.height != Screen.currentResolution.height)
        {

            contentRT.offsetMin = new Vector2(0, - numNiveles * (res.height / porcionVertical));
            res = Screen.currentResolution;
        }
    }
}
