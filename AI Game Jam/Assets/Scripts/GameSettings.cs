using UnityEngine;
/*
Description: Global settings for the game manages the level in player prefs
Author: Chase Bennett-Hill
*/

public static class GameSettings
{
    public static int Level
    {
        get
        {
            return PlayerPrefs.GetInt("Level",-1);
        }
        set
        {
            PlayerPrefs.SetInt("Level", value);
            PlayerPrefs.Save();
        }
    }
}
