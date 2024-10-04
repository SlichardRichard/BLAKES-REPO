using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : Weapon
{
    public float fistslifespan = 1;
    public float Ammo = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void HeldUpdate()
    {
        if (Input.GetMouseButtonDown(0) && player.canFire)
        {
            player.fists.SetActive(true);
            player.StartCoroutine("cooldownFire");
            player.StartCoroutine(DisableFistsRoutine());

        }
    }
    IEnumerator DisableFistsRoutine()
    {
        yield return new WaitForSeconds(fistslifespan);
        player.fists.SetActive(false);
    }
}
