using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{

    public Gun[] guns;

    int currentWeapon = 0;

    public Camera cam;

    public ParticleSystem muzzleFlare;

    public static PlayerWeapons instance;

    public Transform gunsRoot;

    public AudioSource audioSource;

    public Gun currentGun
    {
        get
        {
            return guns[currentWeapon];
        }
    }

    void Awake()
    {
        instance = this;
    }


    public string[] GetWeaponsName()
    {
        string[] names = new string[2];
        for (int i = 0; i < 2; i++)
        {
            names[i] = guns[i].gunName;
        }
        return names;
    }

    void Start()
    {
        guns = new Gun[2];
        for (int i = 0; i < GameManager.instance.save.gunsOnPlayer.Length; i++)
        {
            string gunName = GameManager.instance.save.gunsOnPlayer[i];
            if (gunName == null || gunName == "") continue;
            guns[i] = (Instantiate(Resources.Load<GameObject>("Guns/" + gunName), gunsRoot).GetComponent<Gun>());
        }


        foreach (Gun gun in guns)
        {
            if (gun == null) continue;
            gun.ResetGun();
        }
        ChangeWeapon(0);
        UpdateGunUI();
    }

    public bool CanAddAmmoToMainGun()
    {
        return guns[currentWeapon].CanAddBullets();
    }

    public void AddClipToMainGun()
    {
        guns[currentWeapon].AddBullets(guns[currentWeapon].maxAmmoInMag);
        UpdateGunUI();
    }

    void UpdateGunUI()
    {
        PlayerUI.instance.SetAmmoText(guns[currentWeapon].GetAmmoInMag(), guns[currentWeapon].GetTotalAmmo());
    }

    void ChangeWeapon(int newWeapon)
    {
        guns[currentWeapon].gunModel.SetActive(false);
        currentWeapon = newWeapon;
        guns[currentWeapon].gunModel.SetActive(true);
        muzzleFlare.transform.parent = guns[currentWeapon].barrel.transform;
        muzzleFlare.transform.localPosition = Vector3.zero;
        UpdateGunUI();
    }


    void Update()
    {
        if (guns.Length > 1)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                int newWeapon = (currentWeapon + 1 + guns.Length) % guns.Length;
                if (guns[newWeapon] == null) return;
                ChangeWeapon(newWeapon);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                int newWeapon = (currentWeapon - 1 + guns.Length) % guns.Length;
                if (guns[newWeapon] == null) return;
                ChangeWeapon(newWeapon);
            }
        }


        if (currentGun.CanReload() && Input.GetKeyDown(KeyCode.R))
        {
            guns[currentWeapon].Reload();
            UpdateGunUI();
        }

        guns[currentWeapon].Cooldown();
        if (guns[currentWeapon].CanShoot())
        {
            if ((guns[currentWeapon].autoFireMode && Input.GetMouseButton(0)) ||
            (!guns[currentWeapon].autoFireMode && Input.GetMouseButtonDown(0)))
            {
                Shoot();
                UpdateGunUI();
            }
        }
    }

    void Shoot()
    {
        guns[currentWeapon].Shoot();
        muzzleFlare.Play();
        audioSource.clip = guns[currentWeapon].clip;
        audioSource.Play();

        RaycastHit hit;
        if (Physics.Raycast(transform.position
        , cam.transform.forward
        , out hit, guns[currentWeapon].maxDistance))
        {
            if (hit.collider.tag == "Ennemy" && hit.collider.GetComponent<Terrorist>() != null)
            {
                hit.collider.GetComponent<Terrorist>().TakeDamage(guns[currentWeapon].damage);
            }
        }
    }


    public void AddClipsToWeapon(string weapon, int clips)
    {
        foreach (Gun gun in guns)
        {
            if (gun == null) continue;
            if (gun.gunName.Equals(weapon))
            {
                gun.AddMagazines(clips);
                UpdateGunUI();
                return;
            }
        }

        if (guns[0] != null && guns[1] != null)
        {
            Gun toPutAway = guns[currentWeapon];
            Instantiate(Resources.Load<GameObject>("Guns_Drop/" + toPutAway.gunName), transform.position, Quaternion.identity);
            Destroy(toPutAway.gameObject);
        }
        else
        {
            guns[currentWeapon].gunModel.SetActive(false);
            currentWeapon = 1;
        }

        guns[currentWeapon] = (Instantiate(Resources.Load<GameObject>("Guns/" + weapon), gunsRoot).GetComponent<Gun>());
        guns[currentWeapon].AddMagazines(clips);
        guns[currentWeapon].Reload(false);
        ChangeWeapon(currentWeapon);
        UpdateGunUI();
    }

}
