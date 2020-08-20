using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InterfaceManager : MonoBehaviour
{
    internal const int VOID = -1;
    internal const int BACK = 0;
    internal const int PBACK = 1;
    internal const int MAIN = 2;
    internal const int PAUSE = 3;
    internal const int SETTINGS = 4;
    internal const int LEVELSELECT = 5;

    internal const int NUMGROUPS = 6;

    internal GameManager gameManager;

    internal Animator[] grupos;
    public Animator backgroundGroup;
    public Animator pauseBlurGroup;
    public Animator mainManuGroup;
    public Animator pauseManuGroup;
    public Animator settingsManuGroup;
    public Animator levelSelectManuGroup;

    public Animator loadingGroup;

    public Selectable[] selectDefecto;
    public Selectable backgroundDefSelect;
    public Selectable pauseBlurDefSelect;
    public Selectable mainManuDefSelect;
    public Selectable pauseManuDefSelect;
    public Selectable settingsManuDefSelect;
    public Selectable levelSelectManuDefSelect;

    public Stack<int> historialGrupos = new Stack<int>();
    public Stack<Selectable> historialBotones = new Stack<Selectable>();

    public Selectable lastSelection;

    public RenderTexture loadingTexture;

    public int grupoActual;

    

    private void Update()
    {
        if (gameManager.inputManager.GetButtonDown("Return"))
        {
            ReturnButton();
        }

        if(Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonDown(1) ||
            Input.GetMouseButtonDown(2))
        {
            if(grupoActual != VOID) selectDefecto[grupoActual].Select();
        }
    }
    public virtual void Initialize()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        grupos = new Animator[NUMGROUPS];

        grupos[BACK] = backgroundGroup;
        grupos[PBACK] = pauseBlurGroup;
        grupos[MAIN] = mainManuGroup;
        grupos[PAUSE] = pauseManuGroup;
        grupos[SETTINGS] = settingsManuGroup;
        grupos[LEVELSELECT] = levelSelectManuGroup;

        selectDefecto = new Selectable[NUMGROUPS];

        selectDefecto[BACK] = backgroundDefSelect;
        selectDefecto[PBACK] = pauseBlurDefSelect;
        selectDefecto[MAIN] = mainManuDefSelect;
        selectDefecto[PAUSE] = pauseManuDefSelect;
        selectDefecto[SETTINGS] = settingsManuDefSelect;
        selectDefecto[LEVELSELECT] = levelSelectManuDefSelect;

        StartMainMenu();
        historialGrupos.Push(-1);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void StartMainMenu()
    {      
        CloseAllGroups();
        grupoActual = MAIN;
        OpenGroup(BACK);
        OpenGroup(grupoActual);
        LoadingGroup(false);
    }

    public void OpenPauseMenu()
    {
        OpenGroup(PBACK);
        grupoActual = PAUSE;
        OpenGroup(grupoActual);
    }

    public void ClosePauseMenu()
    {
        CloseGroup(grupoActual);
        grupoActual = VOID;
        CloseGroup(PBACK);
    }

    public void OpenGroup(int indice)
    {
        if (indice != VOID)
        {
            if (indice != BACK && indice != PBACK) selectDefecto[grupoActual].Select();
            grupos[indice].SetBool("Active", true);
        }
    }

    public void CloseAllGroups()
    {
        for(int i = 0; i < grupos.Length; i++)
        {
            CloseGroup(i);
        }
        historialGrupos.Clear();
        historialGrupos.Push(-1);
        historialBotones.Clear();
        grupoActual = VOID;
    }

    public void CloseGroup(int indice)
    {
        if(indice != VOID)
        {
            EventSystem.current.SetSelectedGameObject(null);
            grupos[indice].SetBool("Active", false);
        }
    }

    public void LevelSelectButton()
    {
        historialGrupos.Push(grupoActual);
        ButtonSelect();

        CloseGroup(grupoActual);
        grupoActual = LEVELSELECT;
        OpenGroup(grupoActual);
    }

    public void SettingsButton()
    {
        historialGrupos.Push(grupoActual);
        ButtonSelect();

        CloseGroup(grupoActual);
        grupoActual = SETTINGS;
        OpenGroup(grupoActual);

    }

    public void NewGameButton()
    {
        CloseAllGroups();
        LoadingGroup(true);
        gameManager.NewGame();
    }

    public void ContinueGameButton()
    {
        CloseAllGroups();
        LoadingGroup(true);
        gameManager.ContinueGame();
    }

    public void ContinueButton()
    {
        ClosePauseMenu();
        gameManager.Continue();
    }

    public void ReturnButton()
    {
        if (historialGrupos.Peek() != -1)
        {
            CloseGroup(grupoActual);
            grupoActual = historialGrupos.Pop();
            OpenGroup(grupoActual);
            if (historialBotones.Count != 0)
            {
                historialBotones.Pop().Select();
            }
            else
            {
            }
        }
        else if(grupoActual == PAUSE)
        {
            ContinueButton();
        }
    }

    public void LoadingGroup(bool state)
    {
        if (state)
        {
            loadingGroup.SetBool("Active", true);
        }
        else
        {
            loadingGroup.SetBool("Active", false);
        }
    }

    public void LevelButton(int level)
    {
        CloseAllGroups();
        LoadingGroup(true);
        gameManager.LoadLevel(level);
    }

    public void RestartButton()
    {
        CloseAllGroups();
        LoadingGroup(true);
        gameManager.RestartCurrentLevel();
    }

    public void ExitButton()
    {
        gameManager.ExitGame();
    }

    public void ReturnToMainButton()
    {
        LoadingGroup(true);
        gameManager.ReturnToMain();
    }

    public void ButtonSelect()
    {
        historialBotones.Push(EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>());
    }


}
