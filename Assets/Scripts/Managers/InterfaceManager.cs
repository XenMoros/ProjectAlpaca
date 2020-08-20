using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InterfaceManager : MonoBehaviour
{
    private const int MAIN = 1;
    private const int PAUSE = 2;
    private const int SETTINGS = 3;
    private const int LEVELSELECT = 4;

    internal GameManager gameManager;

    internal Animator[] grupos;
    public Animator backgroundGroup;
    public Animator mainManuGroup;
    public Animator pauseManuGroup;
    public Animator settingsManuGroup;
    public Animator levelSelectManuGroup;

    public Animator loadingGroup;

    public Selectable[] selectDefecto;
    public Selectable backgroundDefSelect;
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
            selectDefecto[grupoActual].Select();
        }
    }
    public virtual void Initialize()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        grupos = new Animator[5];

        grupos[0] = backgroundGroup;
        grupos[1] = mainManuGroup;
        grupos[2] = pauseManuGroup;
        grupos[3] = settingsManuGroup;
        grupos[4] = levelSelectManuGroup;

        selectDefecto = new Selectable[5];

        selectDefecto[0] = backgroundDefSelect;
        selectDefecto[1] = mainManuDefSelect;
        selectDefecto[2] = pauseManuDefSelect;
        selectDefecto[3] = settingsManuDefSelect;
        selectDefecto[4] = levelSelectManuDefSelect;

        StartMainMenu();
        historialGrupos.Push(-1);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void StartMainMenu()
    {
        grupoActual = MAIN;
        CloseAllGroups();
        OpenGroup(0);
        OpenGroup(grupoActual);
        LoadingGroup(false);
    }

    public void OpenPauseMenu()
    {
        OpenGroup(0);
        grupoActual = PAUSE;
        OpenGroup(grupoActual);
    }

    public void ClosePauseMenu()
    {
        CloseGroup(grupoActual);
        CloseGroup(0);
    }

    public void OpenGroup(int indice)
    {
        Debug.Log("Opening " + indice + " group");
        if (indice != 0) selectDefecto[grupoActual].Select();
        grupos[indice].SetBool("Active", true);
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
    }

    public void CloseGroup(int indice)
    {
        Debug.Log("Closing  " + indice + " group");
        EventSystem.current.SetSelectedGameObject(null);
        grupos[indice].SetBool("Active", false);
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
        /*else if(grupoActual == 1)
        {
            ClosePauseMenu();
        }*/
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
