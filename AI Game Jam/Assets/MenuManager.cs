using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button btnContinue;

    // Start is called before the first frame update
    void Start()
    {
        btnContinue.interactable = PlayerPrefs.GetInt("Level") > 0;
    }


    public void QuitGame()
    {
    
        Application.Quit();
    }

    public void NewGame()
    {
        //Do Save Game Stuff
        PlayerPrefs.SetInt("Level", 0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Prologue");
    }

    public void LoadGame()
    {
        switch(PlayerPrefs.GetInt("Level"))
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
}
