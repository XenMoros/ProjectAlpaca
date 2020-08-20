using UnityEngine;
using UnityEngine.UI;

public class DEMOInterfaceManager : InterfaceManager
{

    public override void Initialize()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        grupos = new Animator[NUMGROUPS-1];

        grupos[BACK] = backgroundGroup;
        grupos[PBACK] = pauseBlurGroup;
        grupos[MAIN] = mainManuGroup;
        grupos[PAUSE] = pauseManuGroup;
        grupos[SETTINGS] = settingsManuGroup;

        selectDefecto = new Selectable[NUMGROUPS - 1];

        selectDefecto[BACK] = backgroundDefSelect;
        selectDefecto[PBACK] = pauseBlurDefSelect;
        selectDefecto[MAIN] = mainManuDefSelect;
        selectDefecto[PAUSE] = pauseManuDefSelect;
        selectDefecto[SETTINGS] = settingsManuDefSelect;

        StartMainMenu();
        historialGrupos.Push(-1);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

}
