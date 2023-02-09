using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableIfQuestInProgress : MonoBehaviour
{
    [SerializeField] protected string requiredQuest;

    [SerializeField] protected GameObject objToManage;

    void Start()
    {
        Refresh();
    }

    public virtual void Refresh()
    {
        objToManage.SetActive(GameManager.instance.GetActiveQuest().questID == requiredQuest);
    }
}
