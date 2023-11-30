using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/GrimStunDuration Perk")]
public class ExtendGrimStunPerk : BasePerk
{
    public override void ApplyPerk()
    {
        GameObject.Find("Grim(Clone)").GetComponent<GrimStats>().IncreaseStunDuration(5);
    }


    
}