using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTesting : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
            float damage = gameObject.GetComponentInParent<SlimeStats>().currDamage;

            ps.takeDamage(damage);
        }
        if (collision.gameObject.CompareTag("Ally"))
        {
            AllySkeleton ally = collision.gameObject.GetComponent<AllySkeleton>();
            float damage = gameObject.GetComponentInParent<SlimeStats>().currDamage;
            ally.TakeDamage(damage);
        }
    }
}
