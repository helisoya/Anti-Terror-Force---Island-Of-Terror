using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParentOfPlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if(col.tag!="Player"){
            return;
        }
        PlayerHealth.instance.transform.SetParent(transform);
    }

    void OnTriggerExit(Collider col){
        if(col.tag!="Player"){
            return;
        }
        PlayerHealth.instance.transform.SetParent(null);
    }
}
