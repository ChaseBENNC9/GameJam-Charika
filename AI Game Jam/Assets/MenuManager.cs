using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button btnContinue;

    [SerializeField] private Button btnReset;
    [SerializeField] private GameObject TitleScreenParent;
    [SerializeField] private GameObject OptionScreenParent;

    [SerializeField] private Toggle tglFullscreen;

    // Start is called before the first frame update
    void Start()
    {
        //if the level is negative, the continue button is disabled
        btnContinue.interactable = GameSettings.Level >= 0; 
        btnReset.interactable = GameSettings.Level >= 0;
        ShowTitle();
    }
    public void QuitGame()
    {
    
        Application.Quit();
    }

    public void NewGame()
    {
        //Do Save Game Stuff
        GameSettings.Level = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Prologue");
    }

    public void LoadGame()
    {
        switch(GameSettings.Level)
        {
            case 0:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Prologue");
                break;
            case 1:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
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
        TitleScreenParent.gameObject.SetActive(false);
        OptionScreenParent.gameObject.SetActive(true);
    }
    public void ShowTitle()
    {
        TitleScreenParent.gameObject.SetActive(true);
        OptionScreenParent.gameObject.SetActive(false);
    }
    public void ToggleFullscreen()
    {
        Screen.fullScreen = tglFullscreen.isOn;
    }
}
