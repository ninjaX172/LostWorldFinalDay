using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Hurl Wind Perk")]
public class WindPerk : BasePerk
{
    public override void ApplyPerk()
    {
        //Activate Wind
        GameObject wind = GameObject.Find("Player 1 1(Clone)").transform.Find("Shoot all Direction").gameObject;

        if(wind != null)
        {
            Debug.Log("Hurl Wind Perk");
            wind.SetActive(true);
        }
        
    }
}