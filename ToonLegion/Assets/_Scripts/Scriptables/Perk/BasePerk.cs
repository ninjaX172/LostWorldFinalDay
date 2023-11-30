using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Perk/Base Perk")]
public abstract class BasePerk : ScriptableObject
{
    [Header("Perk Info")]
    public string name;
    public string description;
    public Sprite icon;
    public Rarity rarity;
    public PerkType perkType;
    public abstract void ApplyPerk();
    public bool isRepeatable;
    
}



