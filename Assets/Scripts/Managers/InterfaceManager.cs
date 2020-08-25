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
    internal const int GSETTINGS = 4;
    internal const int ASETTINGS = 5;
    internal const int LEVELSELECT = 6;

    internal const int NUMGROUPS = 7;

    internal GameManager gameManager;

    internal Animator[] grupos;
    public Animator backgroundGroup;
    public Animator pauseBlurGroup;
    public Animator mainManuGroup;
    public Animator pauseManuGroup;
    public Animator gameSettingsGroup;
    public Animator audioSettingsGroup;
    public Animator levelSelectManuGroup;

    public Animator loadingGroup;

    public Selectable[] selectDefecto;
    public Selectable backgroundDefSelect;
    public Selectable pauseBlurDefSelect;
    public Selectable mainManuDefSelect;
    public Selectable pauseManuDefSelect;
    public Selectable gameSettingsManuDefSelect;
    public Selectable audioSettingsManuDefSelect;
    public Selectable levelSelectManuDefSelect;

    public Stack<int> historialGrupos = new Stack<int>();
    public Stack<Selectable> historialBotones = new Stack<Selectable>();

    public Selectable lastSelection;

    public RenderTexture loadingTexture;

    public int grupoActual;

    private Ray ray;
    private RaycastHit hit;

    private void Update()
    {
        if (gameManager.inputManager.GetButtonDown("Return"))
        {
            ReturnButton();
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
        grupos[GSETTINGS] = gameSettingsGroup;
        grupos[ASETTINGS] = audioSettingsGroup;
        grupos[LEVELSELECT] = levelSelectManuGroup;

        selectDefecto = new Selectable[NUMGROUPS];

        selectDefecto[BACK] = backgroundDefSelect;
        selectDefecto[PBACK] = pauseBlurDefSelect;
        selectDefecto[MAIN] = mainManuDefSelect;
        selectDefecto[PAUSE] = pauseManuDefSelect;
        selectDefecto[GSETTINGS] = gameSettingsManuDefSelect;
        selectDefecto[ASETTINGS] = audioSettingsManuDefSelect;
        selectDefecto[LEVELSELECT] = levelSelectManuDefSelect;

        StartMainMenu();
        historialGrupos.Push(-1);
    }

    public void StartMainMenu()
    {      
        CloseAllGroups();
        grupoActual = MAIN;
        OpenGroup(BACK);
        OpenGroup(grupoActual);
        LoadingGroup(false);

        LockCursor(false);

    }

    public void OpenPauseMenu()
    {
        OpenGroup(PBACK);
        grupoActual = PAUSE;
        OpenGroup(grupoActual);

        LockCursor(false);

    }

    public void ClosePauseMenu()
    {
        CloseGroup(grupoActual);
        grupoActual = VOID;
        CloseGroup(PBACK);

        LockCursor(true);

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

    public void GameSettingsButton()
    {
        historialGrupos.Push(grupoActual);
        ButtonSelect();

        CloseGroup(grupoActual);
        grupoActual = GSETTINGS;
        OpenGroup(grupoActual);
    }

    public void AudioSettingsButton()
    {
        historialGrupos.Push(grupoActual);
        ButtonSelect();

        CloseGroup(grupoActual);
        grupoActual = ASETTINGS;
        OpenGroup(grupoActual);
    }

    public void NewGameButton()
    {
        CloseAllGroups();
        LoadingGroup(true);
        gameManager.NewGame();

        LockCursor(true);

    }

    public void ContinueGameButton()
    {
        CloseAllGroups();
        LoadingGroup(true);
        gameManager.ContinueGame();

        LockCursor(true);

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

        LockCursor(true);

    }

    public void RestartButton()
    {
        CloseAllGroups();
        LoadingGroup(true);
        gameManager.RestartCurrentLevel();

        LockCursor(true);

    }

    internal void LockCursor(bool lockState)
    {
        Cursor.lockState = lockState? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockState;
    }

    public void ExitButton()
    {
        gameManager.ExitGame();
    }

    public void ReturnToMainButton()
    {
        LoadingGroup(true);
        gameManager.ReturnToMain();

        LockCursor(true);

    }

    public void ButtonSelect()
    {
        historialBotones.Push(EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>());
    }


}
