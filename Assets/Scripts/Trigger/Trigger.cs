using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public MonoBehaviour[] whatToEnable;

    public bool enabledOneAtATime = false;


    void OnTriggerEnter(Collider col){
        if(col.tag!="Player"){
            return;
        }
        if(whatToEnable.Length != 0){
            StartCoroutine(Enablement());
        }
        this.enabled = false;
    }


    protected IEnumerator Enablement(){
        whatToEnable[0].enabled = true;
        for(int i = 1;i<whatToEnable.Length;i++){
            while(enabledOneAtATime && whatToEnable[i-1].enabled){
                yield return new WaitForEndOfFrame();
            }
            whatToEnable[i].enabled = true;
        }
    }
}
