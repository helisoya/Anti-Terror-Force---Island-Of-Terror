using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditKit : MonoBehaviour
{

    public int amountToRegen = 10;

    private bool playerIsHere = false;


    void Update()
    {
        if(!playerIsHere){
            return;
        }

        if(!PlayerHealth.instance.IsAtMaxHealth()){
            PlayerHealth.instance.AddHealth(amountToRegen);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.tag == "Player"){
            playerIsHere = true;
        }
    }

    void OnTriggerExit(Collider col){
        if(col.tag == "Player"){
            playerIsHere = false;
        }
    }
}
