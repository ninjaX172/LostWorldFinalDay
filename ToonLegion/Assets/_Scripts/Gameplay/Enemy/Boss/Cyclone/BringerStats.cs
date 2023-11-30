using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BringerStats : MonoBehaviour
{

    public BossScriptableObjects EnemyData;
    // Start is called before the first frame update

    float currHealth;
    public float currSpeed;
    public float currDamage;
    bool isDead = false;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private CycloneMovment em;
    public int currentPillar;
    private bool bossStage2;
    public float tempSpeed;

    //Damage popUp
    public GameObject popUpDamagePrefab;
    public TMP_Text damagePopUpText;

    public AudioSource oof;
    void Awake()
    {
        tempSpeed = 0;
        bossStage2 = false;
        currHealth = EnemyData.MaxHealth;
        currSpeed = 0;
        currDamage = EnemyData.Damage;
        currentPillar = EnemyData.Pillar;
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        em = gameObject.GetComponent<CycloneMovment>();
    }

    public void TakeDamage(float damage)
    {
        oof.Play();
        if (currentPillar >= 1)
        {
            damagePopUpText.text = "0";
            Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);
            return; // Immune to damage unless player destroy both pillar.
        }


        damagePopUpText.text = damage.ToString();
        Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);

        currHealth -= damage;
        if (currHealth <= 0 && isDead == false)
        {
            print("Death");
            anim.SetTrigger("Death");
            Kill();
        }
    }

   
    public void Kill()
    {
        PlayerStats ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        ps.enemyKillCounter += 1;
        GetComponent<CycloneMovment>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        var position = gameObject.transform.position; 
        EventManager.Instance.OnBossDeath("Bringer Of Death",new Vector2Int((int)position.x, (int)position.y));

        Destroy(gameObject, 1);
        isDead = true;
    }

    
    public void reduceDamage(float damage)
    {
        float temp = currDamage * damage;
        currDamage = currDamage - temp;
    }
    private void Update()
    {
        if(currHealth <= (EnemyData.MaxHealth / 2) && bossStage2 == false)
        {
            currDamage = currDamage + 15;
            currSpeed = currSpeed + 6;
            bossStage2 = true;
        }
    }


}
