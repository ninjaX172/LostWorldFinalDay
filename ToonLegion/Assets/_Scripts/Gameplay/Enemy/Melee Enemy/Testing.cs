using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    EnemyStats es;
    // Start is called before the first frame update

    
    

   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
            float damage = gameObject.GetComponentInParent<EnemyStats>().currDamage;
           
            ps.takeDamage(damage);
        }
        if (collision.gameObject.CompareTag("Ally"))
        {
            AllySkeleton ally = collision.gameObject.GetComponent<AllySkeleton>();
            float damage = gameObject.GetComponentInParent<EnemyStats>().currDamage;
            ally.TakeDamage(damage);
        }
    }


}
