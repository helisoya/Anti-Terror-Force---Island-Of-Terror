using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableIfQuestDone : EnableIfQuestInProgress
{
    [SerializeField] protected bool enable;
    public override void Refresh()
    {
        if (GameManager.instance.GetQuest(requiredQuest).questStatus == 2)
        {
            objToManage.SetActive(enable);
        }
    }
}
