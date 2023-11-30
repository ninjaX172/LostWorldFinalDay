using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHellHound : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;

    void Start()
    {
        anim = gameObject.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("ENTER");
            //Explode
            float currentDamage = gameObject.GetComponentInParent<HellHoundStats>().currDamage;
            anim.SetBool("isDead", true);
            PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
            ps.takeDamage(currentDamage);
            //anim.SetTrigger("ExplodeDeath");
            Destroy(this.transform.parent.gameObject, 1);
        }
        if (collision.gameObject.CompareTag("Ally"))
        {
            //Explode
            float currentDamage = gameObject.GetComponentInParent<HellHoundStats>().currDamage;

            anim.SetBool("isDead", true);
            AllySkeleton ps = collision.gameObject.GetComponent<AllySkeleton>();
            ps.TakeDamage(currentDamage);
            //anim.SetTrigger("ExplodeDeath");
            Destroy(this.transform.parent.gameObject, 1);
        }
    }
}
