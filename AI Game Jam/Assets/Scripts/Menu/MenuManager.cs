using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
Description: Main menu functionality as well as options and level select
Author: Chase Bennett-Hill

*/

public class MenuManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField]
    private Button btnContinue;

    [SerializeField]
    private Button btnLevelSelect;

    [Header("Screens")]
    [SerializeField]
    private GameObject titleScreenParent;

    [SerializeField]
    private GameObject optionScreenParent;

    [SerializeField]
    private GameObject levelSelectScreenParent;

    [Header("Options Menu")]
    [SerializeField]
    private Toggle tglFullscreen;

    [SerializeField]
    private Button btnReset;

    [Header("Modals")]
    [SerializeField]
    private GameObject newGameModal;

    [Header("Level Select Menu")]
    [SerializeField]
    private Button btnPrologue;

    [SerializeField]
    private Button btnLevel1;

    [SerializeField]
    private Button btnLevel2;

    // Start is called before the first frame update
    void Start()
    {
        btnContinue.interactable = GameSettings.Level >= 0;
        btnReset.interactable = GameSettings.Level >= 0;
        btnLevelSelect.interactable = GameSettings.Level >= 0;
        btnLevel1.interactable = GameSettings.Level >= 1;
        btnLevel2.interactable = false;
        ///Temp Force off
        ShowTitle();
        Debug.Log("Level: " + GameSettings.Level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame(bool force = false)
    {
        if (force)
        {
            GameSettings.Level = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Prologue");
        }
        else
        {
            if (GameSettings.Level >= 0)
            {
                newGameModal.SetActive(true);
            }
            else
            {
                GameSettings.Level = 0;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Prologue");
            }
        }
    }

    public void LoadGame()
    {
        switch (GameSettings.Level)
        {
            case 0:
                LoadLevel("Prologue");
                break;
            case 1:
                LoadLevel("Level 1");
                break;
            default:
                Debug.LogError("No Level Found");
                break;
        }
    }

    public void ResetGame()
    {
        GameSettings.Level = -1;
    }

    public void ShowOptions()
    {
        titleScreenParent.SetActive(false);
        optionScreenParent.SetActive(true);
        levelSelectScreenParent.SetActive(false);
        btnReset.interactable = GameSettings.Level >= 0;
    }

    public void ShowTitle()
    {
        titleScreenParent.SetActive(true);
        optionScreenParent.SetActive(false);
        levelSelectScreenParent.SetActive(false);
        btnContinue.interactable = GameSettings.Level >= 0;
        btnLevelSelect.interactable = GameSettings.Level >= 0;
    }

    public void ShowLevelSelect()
    {
        titleScreenParent.SetActive(false);
        optionScreenParent.SetActive(false);
        levelSelectScreenParent.SetActive(true);
        btnLevel1.interactable = GameSettings.Level >= 1;
        btnLevel2.interactable = false;
        ///Temp Force off
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = tglFullscreen.isOn;
    }

    public void LoadLevel(string levelName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}
