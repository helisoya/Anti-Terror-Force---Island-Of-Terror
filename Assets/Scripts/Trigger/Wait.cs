using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : MonoBehaviour
{
    public float secondsToWait;
    void Start()
    {
        StartCoroutine(WaitForXSeconds());
    }

    IEnumerator WaitForXSeconds(){
        yield return new WaitForSeconds(secondsToWait);
        this.enabled = false;
    }
}
