using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DemonStats : MonoBehaviour
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
    private CorruptedMovement em;


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
        em = gameObject.GetComponent<CorruptedMovement>();
    }

    public void TakeDamage(float damage)
    {
        oof.Play();
        currHealth -= damage;
        damagePopUpText.text = damage.ToString();
        Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);
        anim.SetTrigger("Hurt");
        if (currHealth <= 0 && isDead == false)
        {
            anim.SetBool("Death", true);
            Kill();
        }
    }

    public void BuffEnemy(float damage, float health, float speed)
    {
        currHealth += health;
        if (currHealth > EnemyData.MaxHealth)
        {
            currHealth = EnemyData.MaxHealth;
        }
        currDamage += damage;
        print(currHealth + ", " + currSpeed + ", " + currDamage);
    }

    public void Kill()
    {
        PlayerStats ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        ps.enemyKillCounter += 1;
        GetComponent<CorruptedMovement>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        bc.enabled = false;

        var deathPosition = gameObject.transform.position;
        //EventManager.Instance.OnBossDeath(new Vector2Int((int)deathPosition.x, (int)deathPosition.y));

        Destroy(gameObject, 3);
        isDead = true;


    }
    public void reduceDamage(float damage)
    {
        float temp = currDamage * damage;
        currDamage = currDamage - temp;
    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
    //        ps.takeDamage(currDamage);
    //    }
    //    if (collision.gameObject.CompareTag("Ally"))
    //    {
    //        AllySkeleton ally = collision.gameObject.GetComponent<AllySkeleton>();
    //        ally.TakeDamage(currDamage);
    //    }

    //}




}
