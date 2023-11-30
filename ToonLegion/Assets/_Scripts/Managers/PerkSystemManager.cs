using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerkSystemManager: Singleton<PerkSystemManager>
{
    private List<BasePerk> perks = new();
    private List<BasePerk> availablePerks = new();
    private List<BasePerk> activePerks = new();
    private readonly WeightedRandomSelector<Rarity> rarityWeightedRandomSelector = new();

    //public override void Awake()
    private void Awake()
    {
        //base.Awake();
        perks = Resources.LoadAll<BasePerk>("Perks").ToList();
        availablePerks = new List<BasePerk>(perks);
        
        rarityWeightedRandomSelector.Add(Rarity.Common, 50);
        rarityWeightedRandomSelector.Add(Rarity.Rare, 30);
        rarityWeightedRandomSelector.Add(Rarity.Legendary, 10);
    }

    void OnEnable()
    {
        EventManager.Instance.PerkSelected += EventManager_OnPerkSelected;
        
    }
    
    void OnDisable()
    {
        EventManager.Instance.PerkSelected -= EventManager_OnPerkSelected;
    }
    
    private void EventManager_OnPerkSelected(BasePerk perk)
    {
        ApplyPerk(perk);
    }
    public List<BasePerk> GetRandomPerks(int count)
    {
        count = Mathf.Clamp(count, 0, availablePerks.Count);

        var rarities = new List<Rarity>(
            Enumerable.Range(0, count)
                .Select(x => rarityWeightedRandomSelector.GetRandomWeightedItem())
        );
        
        List<BasePerk> selectedPerks = new();
        
        foreach (var perk in availablePerks)
        {
            if (rarities.Count == 0)
                break;
            if (!rarities.Contains(perk.rarity)) continue;
            selectedPerks.Add(perk);
            rarities.Remove(perk.rarity);
        }
        
        while (selectedPerks.Count < count)
        {
            var temp = availablePerks[Random.Range(0, availablePerks.Count)];
            if (!selectedPerks.Contains(temp))
                selectedPerks.Add(temp);
        }
        
        
        return selectedPerks;
    }

    public void ApplyPerk(BasePerk perk)
    {
        if (!perk.isRepeatable)
        {
            availablePerks.Remove(perk);
        }
        activePerks.Add(perk);
        perk.ApplyPerk();
    }
    public List<BasePerk> GetAllActiveAbilityPerks()
    {
        var toBeReturn = new List<BasePerk>();
        foreach(var perk in activePerks)
        {
            if(perk.perkType == PerkType.Ability)
            {
                toBeReturn.Add(perk);
            }
        }
        return toBeReturn;
    }
    

}
