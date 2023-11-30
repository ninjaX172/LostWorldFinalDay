using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeBehavior : BuffAOEbehavior
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();
            // Buff them
            em.BuffEnemy(buff.DamageBuff, buff.HealthBuff, buff.SpeedBuff);
            
            
        }
        if (collision.CompareTag("Player"))
        {
            PlayerStats p = collision.GetComponent<PlayerStats>();
            p.takeDamage(buff.DamageBuff);
        }
        if(collision.CompareTag("Skeleton Skull"))
        {
            RangeEnemyStats em = collision.GetComponent<RangeEnemyStats>();
            em.BuffEnemy(buff.DamageBuff, buff.HealthBuff, buff.SpeedBuff);
            
        }
        


    }
}
