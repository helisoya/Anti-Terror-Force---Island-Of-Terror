using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWhenDie : Trigger
{
    public GameObject target;


    void Update()
    {
        if(target == null){
            if(whatToEnable.Length != 0){
                StartCoroutine(Enablement());
            }
            this.enabled = false;
        }
    }
}
