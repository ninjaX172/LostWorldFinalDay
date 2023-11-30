using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReduceBehavior : MonoBehaviour
{
    public float damageReduce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();
            em.reduceDamage(damageReduce);
        }
        if (collision.CompareTag("BringerOfDeath"))
        {
            BringerStats bs = collision.GetComponent<BringerStats>();
            bs.reduceDamage(damageReduce);
        }
        if (collision.CompareTag("Skeleton Skull"))
        {
            RangeEnemyStats re = collision.GetComponent<RangeEnemyStats>();
            re.reduceDamage(damageReduce);
        }
        if (collision.CompareTag("Socrerer"))
        {
            NecroStats ns = collision.GetComponent<NecroStats>();
            ns.reduceDamage(damageReduce);
        }
        if (collision.CompareTag("Warlock"))
        {
            CorruptedStats cs = collision.GetComponent<CorruptedStats>();
            cs.reduceDamage(damageReduce);
        }
        
    }
    private void Update()
    {
        Destroy(this.transform.parent.gameObject, 3);
    }

}
