using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDirectionController : MonoBehaviour
{
    [Header("Weapon Stats")]
    //public GameObject prefab;
    //public float damage;
    //public float speed;
    //public float coolDownDuration;
    public WeaponScriptableObject weaponData;
    float currentCooldown;
    //public int pierce;
    protected PlayerMovement pm;


    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        currentCooldown = weaponData.CoolDownDuration; // at start current = cooldownduration
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f && Input.GetKeyDown("space"))
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {
        currentCooldown = weaponData.CoolDownDuration;
    }
}
