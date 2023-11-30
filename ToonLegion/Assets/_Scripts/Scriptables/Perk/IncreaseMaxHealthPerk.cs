using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Increase Max Health Perk")]
public class IncreaseMaxHealth : BasePerk
{
    public override void ApplyPerk()
    {
        PlayerStats x = GameObject.Find("Player 1 1(Clone)").GetComponent<PlayerStats>();
        x.currentMaxHealth += 1;
        Debug.Log("11111");
    }
}