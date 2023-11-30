using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/ReaperLocator Perk")]
public class ReaperLocatorPerk : BasePerk
{
    public override void ApplyPerk()
    {
        GameObject locator = GameObject.Find("Player 1 1(Clone)").transform.Find("Reaper Locator Ability").gameObject;
        if (locator != null)
        {
            locator.SetActive(true);
        }
    }


    
}
