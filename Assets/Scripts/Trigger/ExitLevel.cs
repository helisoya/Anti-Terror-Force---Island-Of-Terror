using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    private bool triggerDone = false;

    void OnTriggerEnter(Collider col)
    {
        if (!triggerDone && col.tag == "Player")
        {
            triggerDone = true;

            GameManager.instance.save.lastMap = SceneManager.GetActiveScene().name;
            PlayerUI.instance.FadeToBlackBeforeLoading("MissionScreen");

            GameManager.instance.save.gunsOnPlayer = PlayerWeapons.instance.GetWeaponsName();
        }
    }
}
