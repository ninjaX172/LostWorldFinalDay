using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HellHoundStats : MonoBehaviour
{
    public EnemyScriptableObject EnemyData;
    // Start is called before the first frame update

    float currHealth;
    float currSpeed;
    public float currDamage;
    bool isDead = false;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    //Damage popUp
    public GameObject popUpDamagePrefab;
    public TMP_Text damagePopUpText;
    public AudioSource oof;

    private void Awake()
    {
        currHealth = EnemyData.MaxHealth;
        currSpeed = EnemyData.MoveSpeed;
        currDamage = EnemyData.Damage;
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
    }

    
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        //Explode

    //        anim.SetBool("isDead", true);
    //        PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
    //        ps.takeDamage(currDamage);
    //        //anim.SetTrigger("ExplodeDeath");
    //        Destroy(gameObject, 1);
    //    }
    //    if (collision.gameObject.CompareTag("Ally"))
    //    {
    //        //Explode

    //        anim.SetBool("isDead", true);
    //        AllySkeleton ps = collision.gameObject.GetComponent<AllySkeleton>();
    //        ps.TakeDamage(currDamage);
    //        //anim.SetTrigger("ExplodeDeath");
    //        Destroy(gameObject, 1);
    //    }
    //}

    public void TakeDamage(float damage)
    {
        oof.Play();

        currHealth -= damage;
        anim.SetTrigger("Hurt");
        damagePopUpText.text = damage.ToString();
        Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);
        if (currHealth <= 0 && isDead == false)
        {
            anim.SetBool("isDead", true);
            Kill();
        }
    }
    public void Kill()
    {
        PlayerStats ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        ps.enemyKillCounter += 1;
        GetComponent<EnemyMovement>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        bc.enabled = false;
        Destroy(gameObject, 3);
        isDead = true;
    }
    public void reduceDamage(float damage)
    {
        float temp = currDamage * damage;
        currDamage = currDamage - temp;
    }

}
