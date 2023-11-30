using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponController : MonoBehaviour
{

    [Header("Weapon Stats")]
    //public GameObject prefab;
    //public float damage;
    //public float speed;
    //public float coolDownDuration;
    public WeaponScriptableObject weaponData;
    float currentCooldown;
    //public int pierce;
     

    protected virtual void Start()
    {
        currentCooldown = weaponData.CoolDownDuration; // at start current = cooldownduration
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {
        currentCooldown = weaponData.CoolDownDuration;
    }

}
