using UnityEngine;
using UnityEngine.UI;

public class DEMOInterfaceManager : InterfaceManager
{

    public override void Initialize()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        grupos = new Animator[4];

        grupos[0] = backgroundGroup;
        grupos[1] = mainManuGroup;
        grupos[2] = pauseManuGroup;
        grupos[3] = settingsManuGroup;

        selectDefecto = new Selectable[4];

        selectDefecto[0] = backgroundDefSelect;
        selectDefecto[1] = mainManuDefSelect;
        selectDefecto[2] = pauseManuDefSelect;
        selectDefecto[3] = settingsManuDefSelect;

        StartMainMenu();
        historialGrupos.Push(-1);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

}
