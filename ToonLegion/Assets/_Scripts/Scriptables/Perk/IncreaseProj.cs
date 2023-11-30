using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/IncreaseProj Perk")]
public class IncreaseProj : BasePerk
{

    public override void ApplyPerk()
    {
        GameObject shotgun = GameObject.Find("Player 1 1(Clone)").transform.Find("Shotgun").gameObject;

        if (shotgun != null)
        {
            shotgun.SetActive(true);
        }
        //Activate apply proj script
        Debug.Log("proj activated");    
    }
}