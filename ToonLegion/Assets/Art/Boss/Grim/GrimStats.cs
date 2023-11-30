using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GrimStats : MonoBehaviour
{

    public EnemyScriptableObject EnemyData;
    // Start is called before the first frame update

    float _currHealth;
    float _currSpeed;
    public float currDamage;
    bool isDead = false;
    private Animator anim;
    private BoxCollider2D bc;
    private EnemyMovement em;

    private float maxHealth = 1f;
    private bool stunned = false;
    private bool canBeDamage = false;


    private float stunTime = 15f;

    private GameObject upperMagicCircle;
    private GameObject lowerMagicCircle;

    private List<GameObject> grimBlessings;

    private GameObject addtionalGrimBlessing;

    private int numOfSoulCollect = 0;
    private int maxSouls = 3;

    private Transform playerTransform;

    private GameObject grimSpawner1;
    private GameObject grimSpawner2;


    void Awake()
    {
        _currHealth = maxHealth;
        _currSpeed = EnemyData.MoveSpeed;
        currDamage = EnemyData.Damage;
        anim = gameObject.GetComponent<Animator>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        em = gameObject.GetComponent<EnemyMovement>();
        playerTransform = FindObjectOfType<PlayerMovement>().transform;

 
    }

    private void OnEnable()
    {
        EventManager.Instance.PlayerDefeatedAllBosses += EventManager_OnPlayerDefeatedAllBosses;
    }

    private void EventManager_OnPlayerDefeatedAllBosses()
    {

        canBeDamage = true;
        maxHealth += 600f;
        _currHealth = maxHealth;
        grimSpawner1.SetActive(true);
        grimSpawner2.SetActive(true);

    }


    private void OnDisable()
    {
        EventManager.Instance.PlayerDefeatedAllBosses -= EventManager_OnPlayerDefeatedAllBosses;
    }

    void Start()
    {

        grimSpawner1 = transform.Find("Grim Spawner").gameObject;
        grimSpawner2 = transform.Find("Grim Spawner (1)").gameObject;


        grimSpawner1.SetActive(false);
        grimSpawner2.SetActive(false);

        upperMagicCircle = transform.Find("UpperMagicCircle").gameObject;
        lowerMagicCircle = transform.Find("LowerMagicCircle").gameObject;
        upperMagicCircle.SetActive(false);
        lowerMagicCircle.SetActive(false);

        grimBlessings = new List<GameObject>
        {
            transform.Find("GrimBlessing1").gameObject,
            transform.Find("GrimBlessing2").gameObject,
            transform.Find("GrimBlessing3").gameObject,
            transform.Find("GrimBlessing4").gameObject,
        };
        addtionalGrimBlessing = transform.Find("GrimBlessingPerk").gameObject;
        foreach (var blessing in grimBlessings)
        {
            blessing.SetActive(false);
        }
        addtionalGrimBlessing.SetActive(false);
    }

    void Update()
    {
        UpdateSpeedBasedOnDistanceFromPlayer();
    }

    public void AddAdditionalGrimBlessing()
    {
        grimBlessings.Add(addtionalGrimBlessing);
    }

    public void IncreaseStunDuration(float duration)
    {
        stunTime += duration;
    }

    private void UpdateSpeedBasedOnDistanceFromPlayer()
    {
        var distance = Vector2.Distance(playerTransform.position, transform.position);
        if (distance > 350f)
        {
            var direction = (playerTransform.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * (distance - 350f);
            transform.position = newPosition;
            return;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
            ps.takeDamage(10000);

        }
    }

    IEnumerator DisableGrimMovementForSeconds(float seconds)
    {
        stunned = true;
        em.enabled = false;
        grimBlessings.RemoveAt(0);
        grimBlessings.ForEach(blessing => blessing.SetActive(true));
        upperMagicCircle.SetActive(true);
        lowerMagicCircle.SetActive(true);

        yield return new WaitForSeconds(seconds);
        em.enabled = true;
        stunned = false;

        grimBlessings.ForEach(blessing => blessing.SetActive(false));
        upperMagicCircle.SetActive(false);
        lowerMagicCircle.SetActive(false);
    }

    public void TakeDamage(float damage)
    {

        if (canBeDamage)
        {
            _currHealth -= damage;
            if(_currHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (stunned)
            {
                return;
            }
            _currHealth -= damage;
            if (_currHealth <= 0 && grimBlessings.Count > 0)
            {

                maxHealth = maxHealth * 1.5f + 10;
                _currHealth = maxHealth;
                _currSpeed += _currSpeed * 0.05f;
                StartCoroutine(DisableGrimMovementForSeconds(stunTime));
            }
        }
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
