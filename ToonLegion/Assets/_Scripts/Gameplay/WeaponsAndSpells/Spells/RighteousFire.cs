using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RighteousFire : MonoBehaviour
{

    public float damageToTake;
    public float damageRate = 1f;
    private float nextDamageTime = 0f;
    private Dictionary<Collider2D, float> nextDamageTimes = new Dictionary<Collider2D, float>();
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && Time.time >= GetNextDamageTime(collision))
        {
            EnemyStats em = collision.gameObject.GetComponent<EnemyStats>();
            em.TakeDamage(damageToTake);
            SetNextDamageTime(collision, Time.time + 1f / damageRate);
        }
        if (collision.CompareTag("Skeleton Skull") && Time.time >= GetNextDamageTime(collision))
        {
            RangeEnemyStats re = collision.gameObject.GetComponent<RangeEnemyStats>();
            re.TakeDamage(damageToTake);
            SetNextDamageTime(collision, Time.time + 1f / damageRate);
        }
        if (collision.CompareTag("BringerOfDeath") && Time.time >= GetNextDamageTime(collision))
        {
            BringerStats bs = collision.gameObject.GetComponent<BringerStats>();
            bs.TakeDamage(damageToTake);
            SetNextDamageTime(collision, Time.time + 1f / damageRate);
        }
        if (collision.CompareTag("Socrerer") && Time.time >= GetNextDamageTime(collision))
        {
            NecroStats ns = collision.gameObject.GetComponent<NecroStats>();
            ns.TakeDamage(damageToTake);
            SetNextDamageTime(collision, Time.time + 1f / damageRate);
        }
        if (collision.CompareTag("Warlock") && Time.time >= GetNextDamageTime(collision))
        {
            CorruptedStats cs = collision.gameObject.GetComponent<CorruptedStats>();
            cs.TakeDamage(damageToTake);
            SetNextDamageTime(collision, Time.time + 1f / damageRate);
        }
        if (collision.CompareTag("FlyingMonster") && Time.time >= GetNextDamageTime(collision))
        {
            FlyingMonsterstats fm = collision.gameObject.GetComponent<FlyingMonsterstats>();
            fm.TakeDamage(damageToTake);
            SetNextDamageTime(collision, Time.time + 1f / damageRate);
        }
        if (collision.CompareTag("SmallEye") && Time.time >= GetNextDamageTime(collision))
        {
            EnemyStats es = collision.gameObject.GetComponent<EnemyStats>();
            es.TakeDamage(damageToTake);
            SetNextDamageTime(collision, Time.time + 1f / damageRate);
        }
        if (collision.CompareTag("HellHound") && Time.time >= GetNextDamageTime(collision))
        {
            HellHoundStats hh = collision.gameObject.GetComponent<HellHoundStats>();
            hh.TakeDamage(damageToTake);
            SetNextDamageTime(collision, Time.time + 1f / damageRate);
        }

    }

    private float GetNextDamageTime(Collider2D enemy)
    {
        if (nextDamageTimes.TryGetValue(enemy, out float nextDamageTime))
        {
            return nextDamageTime;
        }
        else
        {
            return 0f;
        }
    }
    private void SetNextDamageTime(Collider2D enemy, float time)
    {
        nextDamageTimes[enemy] = time;
    }

}
