using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MeleeWeaponBehavior
{

    List<GameObject> markedEnemies;
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy") && !markedEnemies.Contains(collision.gameObject))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();
            em.TakeDamage(currentDamage);
            markedEnemies.Add(collision.gameObject);
        }
        if (collision.CompareTag("BringerOfDeath") && !markedEnemies.Contains(collision.gameObject))
        {
            BringerStats em = collision.GetComponent<BringerStats>();
            em.TakeDamage(currentDamage);
            markedEnemies.Add(collision.gameObject);
        }
        if (collision.CompareTag("Skeleton Skull") && !markedEnemies.Contains(collision.gameObject))
        {
            RangeEnemyStats em = collision.GetComponent<RangeEnemyStats>();
            em.TakeDamage(currentDamage);
            markedEnemies.Add(collision.gameObject);
        }
        if (collision.CompareTag("Warlock") && !markedEnemies.Contains(collision.gameObject))
        {
            CorruptedStats em = collision.GetComponent<CorruptedStats>();
            em.TakeDamage(currentDamage);
            markedEnemies.Add(collision.gameObject);
        }
        if (collision.CompareTag("SmallEye") && !markedEnemies.Contains(collision.gameObject))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();
            em.TakeDamage(currentDamage);
            markedEnemies.Add(collision.gameObject);
        }
        if (collision.CompareTag("FlyingMonster") && !markedEnemies.Contains(collision.gameObject))
        {
            FlyingMonsterstats em = collision.GetComponent<FlyingMonsterstats>();
            em.TakeDamage(currentDamage);
            markedEnemies.Add(collision.gameObject);
        }
        if (collision.CompareTag("Socrerer") && !markedEnemies.Contains(collision.gameObject))
        {
            NecroStats em = collision.GetComponent<NecroStats>();
            em.TakeDamage(currentDamage);
            markedEnemies.Add(collision.gameObject);
        }
        if (collision.CompareTag("HellHound") && !markedEnemies.Contains(collision.gameObject))
        {
            HellHoundStats em = collision.GetComponent<HellHoundStats>();
            em.TakeDamage(currentDamage);
            markedEnemies.Add(collision.gameObject);
        }
        if (collision.CompareTag("PillarDestroy") && !markedEnemies.Contains(collision.gameObject))
        {
            PillarStats em = collision.GetComponent<PillarStats>();
            em.TakeDamage(currentDamage);
            markedEnemies.Add(collision.gameObject);
        }
        if (collision.CompareTag("EyeSpawner") && !markedEnemies.Contains(collision.gameObject))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();
            em.TakeDamage(currentDamage);
            markedEnemies.Add(collision.gameObject);
        }

    }

}
