using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SAVEFILE
{
    public List<string> gunsUnlocked;
    public string[] gunsOnPlayer;

    public Quest[] quests;

    public string lastMap;

    public SAVEFILE()
    {
        gunsUnlocked = new List<string>();
        gunsOnPlayer = new string[2];
        lastMap = "NONE";
    }
}
