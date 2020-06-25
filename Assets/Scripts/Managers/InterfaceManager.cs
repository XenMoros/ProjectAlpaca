using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InterfaceManager : MonoBehaviour
{

    GameManager gameManager;

    CanvasGroup[] grupos = new CanvasGroup[4];
    public CanvasGroup mainManuGroup;
    public CanvasGroup pauseManuGroup;
    public CanvasGroup optionsManuGroup;
    public CanvasGroup levelSelectManuGroup;

    public CanvasGroup loadingGroup;

    public Selectable[] selectDefecto = new Selectable[4];
    public Selectable mainManuDefSelect;
    public Selectable mainManuDefSelect2;
    public Selectable pauseManuDefSelect;
    public Selectable optionsManuDefSelect;
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
    public void Initialize()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        grupos[0] = mainManuGroup;
        grupos[1] = pauseManuGroup;
        grupos[2] = optionsManuGroup;
        grupos[3] = levelSelectManuGroup;

        selectDefecto[0] = mainManuDefSelect;
        selectDefecto[1] = pauseManuDefSelect;
        selectDefecto[2] = optionsManuDefSelect;
        selectDefecto[3] = levelSelectManuDefSelect;

        StartMainMenu();
        historialGrupos.Push(-1);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void StartMainMenu()
    {
        grupoActual = 0;
        CloseAllGroups();
        OpenGroup(grupoActual);
        LoadingGroup(false);
    }

    public void OpenPauseMenu()
    {
        grupoActual = 1;
        OpenGroup(grupoActual);
    }

    public void ClosePauseMenu()
    {
        CloseGroup(grupoActual);
    }

    public void OpenGroup(int indice)
    {
        grupos[indice].alpha = 1;
        grupos[indice].interactable = true;
        selectDefecto[grupoActual].Select();
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
        grupos[indice].alpha = 0;
        grupos[indice].interactable = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void LevelSelectButton()
    {
        historialGrupos.Push(grupoActual);
        ButtonSelect();

        grupoActual = 3;
        OpenGroup(grupoActual);
    }

    public void OptionsButton()
    {
        historialGrupos.Push(grupoActual);
        ButtonSelect();

        grupoActual = 2;
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
                Debug.Log("Boton" + historialBotones.Peek().name);
                historialBotones.Pop().Select();
            }
            else
            {
                Debug.Log("Sin botones??");
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
            loadingGroup.alpha = 1;
        }
        else
        {
            loadingGroup.alpha = 0;
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
        gameManager.ReturnToMain();
        StartMainMenu();
    }

    public void ButtonSelect()
    {
        historialBotones.Push(EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>());
    }


}
