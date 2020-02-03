﻿using UnityEngine;
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

    Selectable[] selectDefecto = new Selectable[4];
    public Selectable mainManuDefSelect;
    public Selectable mainManuDefSelect2;
    public Selectable pauseManuDefSelect;
    public Selectable optionsManuDefSelect;
    public Selectable levelSelectManuDefSelect;


    public Selectable lastSelection;

    public int grupoActivo, grupoAnterior=-1;

    private void Initialize()
    {
        grupos[0] = mainManuGroup;
        grupos[1] = pauseManuGroup;
        grupos[2] = optionsManuGroup;
        grupos[3] = levelSelectManuGroup;

        selectDefecto[0] = mainManuDefSelect;
        selectDefecto[1] = pauseManuDefSelect;
        selectDefecto[2] = optionsManuDefSelect;
        selectDefecto[3] = levelSelectManuDefSelect;

        for (int i=0;i<4;i++)
        {
            ClooseGroup(i);
        }
    }

    public void StartMainMenu()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Initialize();
        OpenGroup(0);
        grupoActivo = 0;
        grupoAnterior = -1;
    }

    public void OpenPauseMenu()
    {
        OpenGroup(1);
        grupoActivo = 1;
        grupoAnterior = -1;
    }

    public void OpenGroup(int indice)
    {
        grupos[indice].alpha = 1;
        grupos[indice].interactable = true;
        if (indice == grupoAnterior)
        {
            lastSelection.Select();
        }
        else
        {
            selectDefecto[indice].Select();
        }

    }

    public void ClooseGroup(int indice)
    {
        grupos[indice].alpha = 0;
        grupos[indice].interactable = false;
    }

    public void LevelSelectButton()
    {
        ButtonSelect();
        OpenGroup(3);
        grupoAnterior = grupoActivo;
        grupoActivo = 3;
    }

    public void OptionsButton()
    {
        ButtonSelect();
        OpenGroup(2);
        grupoAnterior = grupoActivo;
        grupoActivo = 2;

    }

    public void NewGameButton()
    {
        ButtonSelect();
        OpenGroup(1);
        grupoAnterior = grupoActivo;
        grupoActivo = 1;
    }

    public void ContinueGameButton()
    {
        gameManager.ContinueGame();
    }

    public void ContinueButton()
    {
        grupoAnterior = grupoActivo;
        ClooseGroup(grupoActivo);
        gameManager.Continue();
    }

    public void ReturnButton()
    {
        int intermedio;
        OpenGroup(grupoAnterior);
        ClooseGroup(grupoActivo);
        
        intermedio = grupoActivo;
        grupoActivo = grupoAnterior;
        grupoAnterior = intermedio;

    }

    public void LevelButton(int level)
    {
        //
    }

    public void RestartButton()
    {

    }

    public void ExitButton()
    {
        gameManager.ExitGame();
    }

    internal void ButtonSelect()
    {
        lastSelection = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
    }


}