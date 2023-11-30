using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpWind : BossWeaponController
{
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        Vector3 x = new Vector3(0, 1, 0);
        Vector3 rot = new Vector3(0, 0, 90f);
        GameObject spawnSword = Instantiate(weaponData.Prefab);
        spawnSword.transform.position = transform.position;
        spawnSword.GetComponent<WindBehavior_Boss>().AllDirection(x, rot);
    }
}
