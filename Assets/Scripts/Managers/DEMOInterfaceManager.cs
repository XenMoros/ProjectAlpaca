using UnityEngine;
using UnityEngine.UI;

public class DEMOInterfaceManager : InterfaceManager
{

    public override void Initialize()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        grupos = new CanvasGroup[3];
        grupos[0] = mainManuGroup;
        grupos[1] = pauseManuGroup;
        grupos[2] = optionsManuGroup;

        selectDefecto = new Selectable[3];
        selectDefecto[0] = mainManuDefSelect;
        selectDefecto[1] = pauseManuDefSelect;
        selectDefecto[2] = optionsManuDefSelect;

        StartMainMenu();
        historialGrupos.Push(-1);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

}
