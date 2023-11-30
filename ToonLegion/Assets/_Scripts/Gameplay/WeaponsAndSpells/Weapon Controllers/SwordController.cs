using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController
{
    // Start is called before the first frame update
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        

        GameObject spawnSword = Instantiate(weaponData.Prefab);
        spawnSword.transform.position = transform.position;
        spawnSword.GetComponent<SwordBehaviour>().DirectionChecker(pm.lastMovedVector);
    }
}
