using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if(col.tag!="Player"){
            return;
        }
        PlayerHealth.instance.AddHealth(-666);
        this.enabled = false;
    }
}
