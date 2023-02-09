using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndOfMap : MonoBehaviour
{
    public string nextLevel;

    void Start()
    {
        PlayerUI.instance.FadeToBlackBeforeLoading(nextLevel);
        this.enabled = false;
    }

}
