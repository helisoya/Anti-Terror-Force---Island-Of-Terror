using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmBox : MonoBehaviour
{
    // Ajoute l'équivalent d'un chargeur à l'arme principale

    private bool playerIsHere = false;


    void Update()
    {
        if(!playerIsHere){
            return;
        }

        if(PlayerWeapons.instance.CanAddAmmoToMainGun()){
            PlayerWeapons.instance.AddClipToMainGun();
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
