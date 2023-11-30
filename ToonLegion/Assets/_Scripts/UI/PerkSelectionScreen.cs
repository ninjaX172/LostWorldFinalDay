using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PerkSelectionScreen: UIScreen
{
    private TextMeshProUGUI perk1Description;
    private TextMeshProUGUI perk1NameText;
    private Image perk1Image;
    
    private TextMeshProUGUI perk2Description;
    private TextMeshProUGUI perk2NameText;
    private Image perk2Image;
    
    private TextMeshProUGUI perk3Description;
    private TextMeshProUGUI perk3NameText;
    private Image perk3Image;
    
    private readonly List<Button> perkButtons = new List<Button>();
    private List<BasePerk> perks = new List<BasePerk>();
    
    void Start()
    {
        
        if (canvas == null)
        {
            Debug.LogError("Canvas is null!");
        }

        perk1Description =
            canvas.transform.Find("Panel/Perk1/Text Panel/Description").GetComponent<TextMeshProUGUI>();
        perk2Description =
            canvas.transform.Find("Panel/Perk2/Text Panel/Description").GetComponent<TextMeshProUGUI>();
        perk3Description =
            canvas.transform.Find("Panel/Perk3/Text Panel/Description").GetComponent<TextMeshProUGUI>();

        perk1Image =
    canvas.transform.Find("Panel/Perk1/Image Panel/Image").GetComponent<Image>();
        perk2Image =
            canvas.transform.Find("Panel/Perk2/Image Panel/Image").GetComponent<Image>();
        perk3Image =
            canvas.transform.Find("Panel/Perk3/Image Panel/Image").GetComponent<Image>();


        perk1NameText = canvas.transform.Find("Panel/Perk1/Text Panel/Name").GetComponent<TextMeshProUGUI>();
        perk2NameText = canvas.transform.Find("Panel/Perk2/Text Panel/Name").GetComponent<TextMeshProUGUI>();
        perk3NameText = canvas.transform.Find("Panel/Perk3/Text Panel/Name").GetComponent<TextMeshProUGUI>();


        perkButtons.Add(canvas.transform.Find("Panel/Perk1").GetComponent<Button>());
        perkButtons.Add(canvas.transform.Find("Panel/Perk2").GetComponent<Button>());
        perkButtons.Add(canvas.transform.Find("Panel/Perk3").GetComponent<Button>());
        
        foreach (var perkButton in perkButtons)
        {
            perkButton.onClick.AddListener(() =>
            {
                EventManager.Instance.OnPerkSelected(perks[perkButtons.IndexOf(perkButton)]);
            });
            perkButton.enabled = false;
        }
    }
    
    public void OnEnable()
    {
        EventManager.Instance.PerkSelectionScreenTriggered += EventManagerOnPerkSelectionScreenTriggered;
        EventManager.Instance.PerkSelected += EventManager_OnPerkSelected;
    }
    
    public void OnDisable()
    {
        EventManager.Instance.PerkSelectionScreenTriggered -= EventManagerOnPerkSelectionScreenTriggered;
        EventManager.Instance.PerkSelected -= EventManager_OnPerkSelected;
    }
    
    private void EventManagerOnPerkSelectionScreenTriggered()
    {
        Time.timeScale = 0f;

        perks = PerkSystemManager.Instance.GetRandomPerks(3);
        perk1Description.text = perks[0].description;
        perk1Image.sprite = perks[0].icon;
        perk1NameText.text = perks[0].name;


        Debug.Log(perks[0].description);
        perk2Description.text = perks[1].description;
        perk2NameText.text = perks[1].name;
        perk2Image.sprite = perks[1].icon;


        perk3Description.text = perks[2].description;
        perk3NameText.text = perks[2].name;
        perk3Image.sprite = perks[2].icon;


        UIManager.Instance.ShowScreen(ScreenType.PerkSelectionScreen);
        StartCoroutine(EnablePerkButtonsAfterDuration(1f));

    }

    private void EventManager_OnPerkSelected(BasePerk perk)
    {
        Debug.Log("Hello from EventManager_OnPerkSelected");
        foreach (var perkButton in perkButtons)
        {
            perkButton.enabled = false;
        }
        UIManager.Instance.ShowScreen(ScreenType.GameplayScreen);
        
        Time.timeScale = 1f;
        


    }
    IEnumerator EnablePerkButtonsAfterDuration(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        foreach (var perkButton in perkButtons)
        {
            perkButton.enabled = true;
        }
    }
}
