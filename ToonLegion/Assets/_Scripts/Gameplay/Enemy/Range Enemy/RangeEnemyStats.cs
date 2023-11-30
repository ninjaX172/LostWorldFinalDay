using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RangeEnemyStats : MonoBehaviour
{

    public EnemyScriptableObject EnemyData;
    // Start is called before the first frame update

    float currHealth;
    float currSpeed;
    float currDamage;
    bool isDead = false;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    //Damage popUp
    public GameObject popUpDamagePrefab;
    public TMP_Text damagePopUpText;
    public AudioSource oof;

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
        oof.Play();
        damagePopUpText.text = damage.ToString();
        Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);
        anim.SetTrigger("Hurt");
        currHealth -= damage;
        if (currHealth <= 0 && isDead == false)
        {
            anim.SetBool("isDead", true);
            Kill();
        }
    }
    public void reduceDamage(float damage)
    {
        float temp = currDamage * damage;
        currDamage = currDamage - temp;
    }
    public void BuffEnemy(float damage, float health, float speed)
    {
        currHealth += health;
        if (currHealth > EnemyData.MaxHealth)
        {
            currHealth = EnemyData.MaxHealth;
        }
        currDamage += damage;
    }

    public void Kill()
    {
        PlayerStats ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        ps.enemyKillCounter += 1;
        GetComponent<RangeEnemyMovement>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        bc.enabled = false;
        Destroy(gameObject, 1);
        isDead = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
            ps.takeDamage(currDamage);
        }
        if (collision.gameObject.CompareTag("Ally"))
        {
            AllySkeleton ps = collision.gameObject.GetComponent<AllySkeleton>();
            ps.TakeDamage(currDamage);
        }
    }

}
