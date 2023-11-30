using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/SoulStealer Perk")]
public class SoulStealerPerk : BasePerk
{
    public override void ApplyPerk()
    {
        GameObject ability = GameObject.Find("Player 1 1(Clone)").transform.Find("Soul Steal Ability").gameObject;
        if (ability != null)
        {
            ability.SetActive(true);
        }
    }


    
}
