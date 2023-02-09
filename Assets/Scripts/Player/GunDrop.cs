using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDrop : MonoBehaviour
{
    public string weaponName;
    public int clips;

    private bool mouseIn;

    private bool distanceOkay;

    void Start()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), PlayerWeapons.instance.GetComponentInChildren<Collider>(), true);
    }

    void OnMouseEnter()
    {
        mouseIn = true;
    }

    void OnMouseExit()
    {
        PlayerUI.instance.SetHint("", Color.white);
        mouseIn = false;
        distanceOkay = false;

    }


    void Update()
    {
        if (mouseIn && !distanceOkay && Vector3.Distance(transform.position, PlayerWeapons.instance.transform.position) <= 3)
        {
            distanceOkay = true;
            PlayerUI.instance.SetHint("Press e to pickup", Color.white);
        }

        if (mouseIn && distanceOkay && Input.GetKeyDown(KeyCode.E))
        {
            PlayerUI.instance.SetHint("", Color.white);
            PlayerWeapons.instance.AddClipsToWeapon(weaponName, clips);
            Destroy(gameObject);
        }
    }
}
