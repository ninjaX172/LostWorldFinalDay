using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    public BossScriptableObjects EnemyData;
    float currDamage_;

    private void Update()
    {
    }
    private void Awake()
    {
        currDamage_ = this.transform.parent.gameObject.GetComponent<BringerStats>().currDamage;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
            ps.takeDamage(currDamage_);
        }
        if (collision.gameObject.CompareTag("Ally"))
        {
            AllySkeleton ps = collision.gameObject.GetComponent<AllySkeleton>();
            ps.TakeDamage(currDamage_);
        }
    }

}
