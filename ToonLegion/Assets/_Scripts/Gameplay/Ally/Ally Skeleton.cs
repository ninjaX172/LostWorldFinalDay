using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySkeleton : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyScriptableObject EnemyData;
    // Start is called before the first frame update

    float currHealth;
    float currSpeed;
    float currDamage;
    bool isDead = false;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    void Awake()
    {
        currHealth = EnemyData.MaxHealth;
        currSpeed = EnemyData.MoveSpeed;
        currDamage = EnemyData.Damage;
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
    }
    public void TakeDamage(float damage)
    {
        anim.SetTrigger("Hurt");
        currHealth -= damage;
        if (currHealth <= 0 && isDead == false)
        {
            anim.SetBool("isDead", true);
            Kill();
        }
    }
    public void Kill()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        bc.enabled = false;
        Destroy(gameObject, 1);
        isDead = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStats ps = collision.gameObject.GetComponent<EnemyStats>();
            ps.TakeDamage(currDamage);
        }
        if (collision.gameObject.CompareTag("Skeleton Skull"))
        {
            RangeEnemyStats ps = collision.gameObject.GetComponent<RangeEnemyStats>();
            ps.TakeDamage(currDamage);
        }
        if (collision.gameObject.CompareTag("BringerOfDeath"))
        {
            BringerStats ps = collision.gameObject.GetComponent<BringerStats>();
            ps.TakeDamage(currDamage);
        }
        if (collision.gameObject.CompareTag("Socrerer"))
        {
            NecroStats ps = collision.gameObject.GetComponent<NecroStats>();
            ps.TakeDamage(currDamage);
        }
        if (collision.gameObject.CompareTag("Warlock"))
        {
            CorruptedStats ps = collision.gameObject.GetComponent<CorruptedStats>();
            ps.TakeDamage(currDamage);
        }
        if (collision.gameObject.CompareTag("FlyingMonster"))
        {
            FlyingMonsterstats ps = collision.gameObject.GetComponent<FlyingMonsterstats>();
            ps.TakeDamage(currDamage);
        }
        if (collision.gameObject.CompareTag("HellHound"))
        {
            HellHoundStats ps = collision.gameObject.GetComponent<HellHoundStats>();
            ps.TakeDamage(currDamage);
        }
        if (collision.gameObject.CompareTag("SmallEye"))
        {
            EnemyStats ps = collision.gameObject.GetComponent<EnemyStats>();
            ps.TakeDamage(currDamage);
        }


    }

    private void Update()
    {
        Destroy(gameObject, 8);
    }
}
