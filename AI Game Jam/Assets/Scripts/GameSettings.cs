using UnityEngine;

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
