using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gun : Weapon
{
    public GameObject shot;
    public float shotspeed = 0f;
    public float maxAmmo = 0;
    public float currentAmmo = 0;
    public float ReloadAmount = 0;
    public int Firemode = 0;
    public float bulletlifespan = 0;
    public PlayerController playerData;

    public float bulletraylength = 1f;
    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public override void HeldUpdate()
    {
        //semiauto fire
        if (Input.GetMouseButtonDown(0) && player.canFire && Firemode >= 1 && currentAmmo > 0)
        {
            GameObject s = GameObject.Instantiate(shot, player.Weaponslot.position, player.Weaponslot.rotation);
            s.GetComponent<Rigidbody>().AddForce(player.playerCam.transform.forward * shotspeed);
            currentAmmo--;
            GameObject.Destroy(s, bulletlifespan);

            player.canFire = false;
            player.StartCoroutine("cooldownFire");

        }
        //autofire
        if (Input.GetMouseButton(0) && player.canFire && Firemode >= 2 && currentAmmo > 0)
        {
            GameObject s = GameObject.Instantiate(shot, player.Weaponslot.position, player.Weaponslot.rotation);
            s.GetComponent<Rigidbody>().AddForce(player.playerCam.transform.forward * shotspeed);
            currentAmmo--;
            GameObject.Destroy(s, bulletlifespan);


            player.canFire = false;
            player.StartCoroutine("cooldownFire");
        }
        //HitscanFire
        /*
        if (Input.GetMouseButton(0) && player.canFire && Firemode >= 3 && currentAmmo > 0)
        {
           Physics.Raycast(weaponslot.transform.position, weaponslot.transform.forward, out RaycastHit hit, 5f);


        }
       */
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            reloadClip();
        }
    
    }
    public void reloadClip()
    {


        if (currentAmmo >= maxAmmo)
            return;

        else
        {
            float reloadCount = maxAmmo - currentAmmo;

            if (currentAmmo < reloadCount)
            {
                currentAmmo += currentAmmo;
                currentAmmo = 0;
                return;
            }

            else
            {
                currentAmmo += reloadCount;
                currentAmmo -= reloadCount;
                return;
            }
        }

    }
}
