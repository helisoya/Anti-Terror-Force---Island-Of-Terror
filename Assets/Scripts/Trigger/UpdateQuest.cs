using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateQuest : MonoBehaviour
{
    [SerializeField] private string questID;
    [SerializeField] private int status;
    [SerializeField] private bool refreshAll;

    void Start()
    {
        GameManager.instance.GetQuest(questID).questStatus = status;

        if (refreshAll)
        {
            EnableIfQuestInProgress[] toRefresh = FindObjectsOfType<EnableIfQuestInProgress>();
            foreach (EnableIfQuestInProgress script in toRefresh)
            {
                script.Refresh();
            }
        }

        this.enabled = false;
    }
}
