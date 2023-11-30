using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Claustrophobia Perk")]
public class ClaustrophobiaPerk: BasePerk
{
    public override void ApplyPerk()
    {
        PlayerStats ps = FindAnyObjectByType<PlayerStats>();
        ps.currentMoveSpeed += 1;
        //ps.damage += 5;
        Debug.Log("Claustrophobia");
    }
}