using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionScreen_HideIfMissionNotDone : MonoBehaviour
{
    [SerializeField] private string missionRequirement;

    void Start()
    {
        gameObject.SetActive(GameManager.instance.GetQuest(missionRequirement).questStatus == 2);
    }
}
