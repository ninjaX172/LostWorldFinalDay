using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NecroStats : MonoBehaviour
{

    public EnemyScriptableObject EnemyData;
    // Start is called before the first frame update

    float currHealth;
    float currSpeed;
    float currDamage;
    bool isDead = false;
    private Animator anim;
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
        bc = gameObject.GetComponent<BoxCollider2D>();

    }

    public void TakeDamage(float damage)
    {
        oof.Play();

        damagePopUpText.text = damage.ToString();

        Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);
        currHealth -= damage;
        if (currHealth <= 0 && isDead == false)
        {
            anim.SetTrigger("Death");
            Kill();
        }
    }


    public void reduceDamage(float damage)
    {
        float temp = currDamage * damage;
        currDamage = currDamage - temp;
    }
    public void Kill()
    {
        PlayerStats ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        ps.enemyKillCounter += 1;
        GetComponent<NecroMovement>().enabled = false;
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
            AllySkeleton a = collision.gameObject.GetComponent<AllySkeleton>();
            a.TakeDamage(currDamage);
        }
    }

}
