using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Perk/Sighted Perk")]
public class SightedPerk: BasePerk
{
    public override void ApplyPerk()
    {
        ////Increase light radius
        //Light2D light = GameObject.Find("Player 1 1(Clone)").transform.Find("Player Light Source(Clone)").gameObject.GetComponent<Light2D>();

        //light.pointLightOuterRadius = 16;
        PlayerStats x = GameObject.Find("Player 1 1(Clone)").GetComponent<PlayerStats>();
        x.currentMoveSpeed += 2;

        //Debug.Log("Sighted Perk");    
    }
}