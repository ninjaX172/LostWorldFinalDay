using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PillarStats : MonoBehaviour
{

    public EnemyProp EnemyData;
    // Start is called before the first frame update

    float currHealth;
    
    bool isDead = false;
    private BoxCollider2D bc;

    //Damage popUp
    public GameObject popUpDamagePrefab;
    public TMP_Text damagePopUpText;
    void Awake()
    {
        currHealth = EnemyData.MaxHealth;
    
        bc = gameObject.GetComponent<BoxCollider2D>();
    }

    public void TakeDamage(float damage)
    {
        damagePopUpText.text = damage.ToString();
        print(transform.position);

        Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);
        currHealth -= damage;
        if (currHealth <= 0 && isDead == false)
        {
            print("Death");
            Kill();
        }
    }

   

    public void Kill()
    {
        bc.enabled = false;
        Destroy(gameObject);
        BringerStats x = GetComponentInParent<BringerStats>();
        x.currentPillar = x.currentPillar - 1;
       
        isDead = true;
    }

    



}
