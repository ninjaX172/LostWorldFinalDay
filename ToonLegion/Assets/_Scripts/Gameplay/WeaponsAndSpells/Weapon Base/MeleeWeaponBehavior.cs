using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehavior : MonoBehaviour
{
    //Base script for all melee behaviours
    // Start is called before the first frame update

    public WeaponScriptableObject weaponData;


    public float destroyAfterSeconds;
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CoolDownDuration;
        currentPierce = weaponData.Pierce;
    }

    // Update is called once per frame
    protected virtual void Start()
    {
        print(1111);
        Destroy(gameObject, destroyAfterSeconds);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            print("Taking Firedamage");
            EnemyStats em = collision.GetComponent<EnemyStats>();
            em.TakeDamage(currentDamage);
            
        }
    }
    //protected virtual void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        print("Taking Firedamage");
    //        EnemyStats em = collision.GetComponent<EnemyStats>();
    //        em.TakeDamage(currentDamage);

    //    }
    //}

}
