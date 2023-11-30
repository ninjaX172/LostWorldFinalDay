using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PauseScreen : UIScreen
{
    // Perk screen


    private List<TextMeshProUGUI> PerkUI = new List<TextMeshProUGUI>();
    //Enemy Kill Counter
    private TextMeshProUGUI statsScreen;
    private TextMeshProUGUI speedStat;
    private GameObject description;
    private TextMeshProUGUI abilityDescribe;
    private readonly List<Button> perkButtons = new List<Button>();
    private List<BasePerk> perks = new List<BasePerk>();
    private Button hardModeButton;

    private void Start()

    {
        description = canvas.transform.Find("Description").gameObject;
        abilityDescribe = canvas.transform.Find("Description/Text (TMP)").GetComponent<TextMeshProUGUI>();
        statsScreen = canvas.transform.Find("Interactable Panel/Stat/EnemyCounter").GetComponent<TextMeshProUGUI>();
        for (int i = 1; i <= 17; i++)
        {
            var x = canvas.transform.Find("Interactable Panel/Image/Scroll/Panel/Perk" + i.ToString()).GetComponent<TextMeshProUGUI>();

            x.enabled = false;
            PerkUI.Add(x);
        }



        hardModeButton = canvas.transform.Find("Death Mode Button/Text").GetComponent<Button>();
        hardModeButton.onClick.AddListener(() =>
        {
            Debug.Log("Hard Mode");
            EventManager.Instance.OnPlayerEnteredGrimTrial();
            hardModeButton.interactable = false;
        });
    }



    public void OnEnable()
    {
        EventManager.Instance.PauseScreenTriggered += EventManager_OnPauseScreenTriggered;
        EventManager.Instance.PauseScreenClosed += EventManager_OnPauseScreenClosed;
    }

    private void EventManager_OnPauseScreenClosed()
    {
        Time.timeScale = 1f;
        description.SetActive(false);
        UIManager.Instance.ShowScreen(ScreenType.GameplayScreen);
    }

    public void OnDisable()
    {
        EventManager.Instance.PauseScreenTriggered -= EventManager_OnPauseScreenTriggered;
        EventManager.Instance.PauseScreenClosed -= EventManager_OnPauseScreenClosed;

    }

    private void EventManager_OnPauseScreenTriggered()
    {
        PlayerStats ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Time.timeScale = 0f;
        perks = PerkSystemManager.Instance.GetAllActiveAbilityPerks();
        for (int i = 0; i < perks.Count; i++)
        {
            var perkButton = canvas.transform.Find("Interactable Panel/Image/Scroll/Panel/Perk" + (i + 1).ToString()).GetComponent<Button>();
            perkButton.onClick.AddListener(() =>
            {
                string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
                string numberString = System.Text.RegularExpressions.Regex.Replace(buttonName, "[^0-9]", "");
                int number = int.Parse(numberString);
                onClickShowDescription(perks[number - 1].description);
            });
            perkButtons.Add(perkButton);

            PerkUI[i].text = perks[i].name;
            PerkUI[i].enabled = true;
        }
        statsScreen.text = "Enemy Killed: " + ps.enemyKillCounter;

        UIManager.Instance.ShowScreen(screenType);
    }

    public void onClickShowDescription(string x)
    {
        abilityDescribe.text = x;
        description.SetActive(true);
    }
    public void onClickExit()
    {
        description.SetActive(false);
    }

    public void boss1()
    {
        description.SetActive(true);
        abilityDescribe.text = "THE BRINGER OF DEATH HOLDS THREE PILLARS, ALL MUST BE DESTROYED TO HARM THEM. THEY UNLEASH A PSYCHIC RAGE WHEN HP DROPS BELOW A SET POINT.";
    }
    public void boss2()
    {
        description.SetActive(true);
        abilityDescribe.text = "WARLOCK HAVE 3 EYEBALLS THAT SPAWNS ENEMY SKELETON BEST TO KILL THE SPAWNERS BEFORE ATTACKING IT";
    }
    public void boss3()
    {
        description.SetActive(true);
        abilityDescribe.text = "FALLEN HERO ONCE KILL DEMON WILL SPAWN";
    }

    public void returnToMenu()
    {
        Application.Quit();
    }


}
