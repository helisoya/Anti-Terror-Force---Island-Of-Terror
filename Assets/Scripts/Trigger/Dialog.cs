using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{

    public string[] texts;
    public Color[] colors;

    void Start(){
        PlayerUI.instance.SetDialog(texts,colors);
        StartCoroutine(WaitForEndOfDialog());
    }

    IEnumerator WaitForEndOfDialog(){
        while(PlayerUI.instance.dialogIsInProgress){
            yield return new WaitForEndOfFrame();
        }
        this.enabled = false;
    }
}
