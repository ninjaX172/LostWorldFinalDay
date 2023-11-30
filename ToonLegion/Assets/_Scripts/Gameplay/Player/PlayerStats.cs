using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    //private Text Health;
    //private Text Might;
    //private Text Speed;
    
    public CharacterScriptableObject characterData;


    private Animator anim;
    private PlayerMovement pm;
    private BoxCollider2D bc;
    private Rigidbody2D rb;

    public float currentHealth;
    public float currentMaxHealth;
    public float currentMight;
    public float currentRecovery;
    public float currentProjectileSpeed;
    public float currentMoveSpeed;
    public float currentMagnet;
    public float damage;

    public int enemyKillCounter;

    //Experiencce 
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    //I-Frame
    [Header("I-Frames")]
    public float invincibleDuration;
    float invincibilityTimer;
    bool isInvincible;

    bool Alive = true;


    //Hit
    public AudioSource oof;

    private List<Image> hearts = new List<Image>();
    GameObject please;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;
    // Perks
    private GameObject soulStealPerk;

    void Awake()
    {
        //Health = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
        //Might = GameObject.FindGameObjectWithTag("Might").GetComponent<Text>();
        //Speed = GameObject.FindGameObjectWithTag("Speed").GetComponent<Text>();
        enemyKillCounter = 0;

        currentMight = characterData.Might;
        currentMaxHealth = characterData.MaxHealth;
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMoveSpeed = 0;
        currentMagnet = characterData.Magnet;
        anim = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        bc = GetComponent<BoxCollider2D>();
        damage = 2;

        please = GameObject.FindGameObjectWithTag("Hearts");

        for(int i = 1; i < please.transform.childCount; i++)
        {
            hearts.Add(please.transform.GetChild(i).GetComponent<Image>());
        }
    }


    private void Start()
    {
        soulStealPerk = GameObject.Find("Soul Steal Ability");
        soulStealPerk.SetActive(false);

        experienceCap = levelRanges[0].experienceCapIncrease;
    }


    private void OnEnable()
    {
        EventManager.Instance.PlayerEnteredGrimTrial += EventManager_OnPlayerEnteredGrimTrial;
    }

    private void OnDisable()
    {
        EventManager.Instance.PlayerEnteredGrimTrial -= EventManager_OnPlayerEnteredGrimTrial;

    }

    private void EventManager_OnPlayerEnteredGrimTrial()
    {
        currentMaxHealth = 1;
        currentHealth = 1;
        please.SetActive(false);


    }


    private void Update()
    {
        //Health.text = "Health: " + Math.Clamp(currentHealth, 0,Int32.MaxValue);
        //Might.text = "Might: " + currentMight;
        //Speed.text = "Speed: " + currentMoveSpeed;

        //EventManager.Instance.OnPlayerHealthChange((int)currentHealth, (int)currentMaxHealth);

        if (GameManager.Instance.IsGrimTrial)
        {

            currentMaxHealth = 1;
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < currentMaxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

        }


        if(invincibilityTimer > 0) // We know that player needs to decrememt until timer is 0.
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible) // Okay after the duration is 0 we check if our invincible status is still true. If so we change it to false. No our player is reset and can be damage again.
        {
            isInvincible = false;
        }

        if (currentMight >= 4)
        {
            currentMight = 0;
            EventManager.Instance.OnPerkSelectionScreenTriggered();
            Debug.Log(1);
        }

        currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, 0, 3);
        
        
        recover();
    }

    void LevelUpChecker()
    {
        if(experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            int experienceCapIncrease = 0;
            foreach(LevelRange i in levelRanges)
            {
                if (i.startLevel <= level && i.endLevel >= level)
                {
                    experienceCapIncrease = i.experienceCapIncrease;
                    break;
                }
            }
            experienceCap = experienceCap + experienceCapIncrease;
        }



    }

    public void takeDamage(float damage) // This function will decrease player current hp by the enemy damage output. For instance if player collide with enemy. This function will be call from another script.
    {
        
        if (!isInvincible && Alive)
        {
            oof.Play();
            currentHealth = currentHealth - damage;
            invincibilityTimer = invincibleDuration; // Set timer to duration so people is immune for that given time. Timer will decrease until 0 and then player can attack again.
            isInvincible = true;


            // This check if player is killed or not.
            if (currentHealth <= 0)
            {
                if (soulStealPerk.activeInHierarchy)
                {
                    if (soulStealPerk.GetComponent<SoulStealerAbility>().attemptToUseAbility())
                    {
                        currentHealth = currentMaxHealth / 2f;
                        return;
                    }
                }
                kill();
            }
        }
        
    }

    public void kill()
    {
        anim.SetTrigger("Death");
        Alive = false;
        bc.enabled = false;
        pm.enabled = false;
        Destroy(gameObject,1);

    }

    void recover()
    {
        if (GameManager.Instance.IsGrimTrial)
        {
            return;
        }
        if (currentHealth < characterData.MaxHealth)
        {
            // Increase current health by recover rate
            currentHealth = currentHealth + (currentRecovery * .6f) * Time.deltaTime;
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

}
