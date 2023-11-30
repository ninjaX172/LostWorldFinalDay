using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/AdditionalGrimStun Perk")]
public class AdditionalGrimStunPerk : BasePerk
{
    public override void ApplyPerk()
    {
        GameObject.Find("Grim(Clone)").GetComponent<GrimStats>().AddAdditionalGrimBlessing();
    }


    
}
