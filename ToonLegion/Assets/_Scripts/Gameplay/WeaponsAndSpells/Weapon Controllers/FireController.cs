using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnFireShield = Instantiate(weaponData.Prefab);
        spawnFireShield.transform.position = transform.position; //Assign position
        spawnFireShield.transform.parent = transform; // Spawn below object
    }
}
