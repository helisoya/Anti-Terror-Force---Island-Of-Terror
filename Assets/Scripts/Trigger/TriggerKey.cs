using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKey : Trigger
{
    public KeyCode keyToPress = KeyCode.Return;

    private bool playerHere = false;

    void Update()
    {
        if (playerHere && Input.GetKeyDown(keyToPress))
        {
            PlayerUI.instance.SetHint("", new Color(0, 0, 0, 0));
            if (whatToEnable.Length != 0)
            {
                StartCoroutine(Enablement());
            }
            this.enabled = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (enabled && col.tag == "Player")
        {
            playerHere = true;
            PlayerUI.instance.SetHint("Press " + keyToPress.ToString(), Color.white);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (enabled && col.tag == "Player")
        {
            playerHere = false;
            PlayerUI.instance.SetHint("", new Color(0, 0, 0, 0));
        }
    }
}
