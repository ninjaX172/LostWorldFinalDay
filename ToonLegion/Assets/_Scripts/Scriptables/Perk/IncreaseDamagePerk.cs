using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Increase Damage Perk")]
public class IncreaseDamagePerk : BasePerk
{
    public override void ApplyPerk()
    {
        PlayerStats ps = GameObject.FindAnyObjectByType<PlayerStats>();
        ps.damage += 5;
        Debug.Log("Increase Damage for basic auto");


    }
}