using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordContollerLeft : SpecialAblityDirection
{
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        Vector3 x = new Vector3(-1, 0, 0);

        GameObject spawnSword = Instantiate(weaponData.Prefab);
        spawnSword.transform.position = transform.position;
        spawnSword.GetComponent<SwordBehaviour>().DirectionChecker(x);
    }

}
