using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Debuff Perk")]
public class DebuffPerk : BasePerk
{
    public override void ApplyPerk()
    {
        //Set debuff Zone true

        GameObject debuff = GameObject.Find("Player 1 1(Clone)").transform.Find("Debuff Ability").gameObject;

        if (debuff != null)
        {
            debuff.SetActive(true);
        }
        Debug.Log("Debuff Zone activated");
    }
}