using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questID;


    // 0 = Not Started
    // 1 = In Progress
    // 2 = Done
    public int questStatus;

    public string questName;
    public string questDescription;

    public Quest(Quest toCopy)
    {
        questID = toCopy.questID;
        questStatus = 0;
        questName = toCopy.questName;
        questDescription = toCopy.questDescription;
    }
}
