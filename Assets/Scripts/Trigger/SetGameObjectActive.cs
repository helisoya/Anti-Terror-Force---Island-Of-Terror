using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectActive : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public bool value = true;
    void Start()
    {
        foreach(GameObject obj in objectsToActivate){
            obj.SetActive(value);
        }
        this.enabled = false;
    }

}
