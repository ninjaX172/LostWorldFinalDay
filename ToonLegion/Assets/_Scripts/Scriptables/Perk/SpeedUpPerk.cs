using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Speed Up Perk")]
public class SpeedUpPerk : BasePerk
{
    public override void ApplyPerk()
    {
        PlayerStats x = GameObject.Find("Player 1 1(Clone)").GetComponent<PlayerStats>();
        x.currentMoveSpeed += 1;
        Debug.Log("11111");

    }
}
