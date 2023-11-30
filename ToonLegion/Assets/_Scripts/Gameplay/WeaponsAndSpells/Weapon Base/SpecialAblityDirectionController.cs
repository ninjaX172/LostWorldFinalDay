using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAblityDirection : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Weapon Stats")]
    
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
