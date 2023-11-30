using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    public float debuffPerctange;
    public float toBeDestroy;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, toBeDestroy);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Reduce attack by perctange.
        if (collision.CompareTag("Enemy"))
        {
            print(debuffPerctange / 100);
            EnemyStats em = collision.gameObject.GetComponent<EnemyStats>();
            em.reduceDamage(debuffPerctange/ 100);
        }
        if (collision.CompareTag("Skeleton Skull"))
        {
            RangeEnemyStats re = collision.gameObject.GetComponent<RangeEnemyStats>();
            re.reduceDamage(debuffPerctange / 100);
        }
        if (collision.CompareTag("BringerOfDeath"))
        {
            BringerStats bs = collision.gameObject.GetComponent<BringerStats>();
            bs.reduceDamage(debuffPerctange / 100);
        }
        if (collision.CompareTag("Socrerer") )
        {
            NecroStats ns = collision.gameObject.GetComponent<NecroStats>();
            ns.reduceDamage(debuffPerctange / 100);
        }
        if (collision.CompareTag("Warlock") )
        {
            CorruptedStats cs = collision.gameObject.GetComponent<CorruptedStats>();
            cs.reduceDamage(debuffPerctange / 100);
        }
        if (collision.CompareTag("FlyingMonster") )
        {
            FlyingMonsterstats fm = collision.gameObject.GetComponent<FlyingMonsterstats>();
            fm.reduceDamage(debuffPerctange / 100);
        }
        if (collision.CompareTag("SmallEye") )
        {
            EnemyStats es = collision.gameObject.GetComponent<EnemyStats>();
            es.reduceDamage(debuffPerctange / 100);
            
        }
        if (collision.CompareTag("HellHound"))
        {
            HellHoundStats hh = collision.gameObject.GetComponent<HellHoundStats>();
            hh.reduceDamage(debuffPerctange / 2);
        }


    }
}
