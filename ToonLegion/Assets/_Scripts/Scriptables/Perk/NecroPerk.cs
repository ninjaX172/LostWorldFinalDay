using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Necro Perk")]
public class NecroPerk : BasePerk
{
    public override void ApplyPerk()
    {
        GameObject Necro = GameObject.Find("Player 1 1(Clone)").transform.Find("Necro Ability").gameObject;
        if (Necro != null)
        {
            Necro.SetActive(true);
        }
        //Activate apply proj script
        Debug.Log("Necro activated");

    }


    
}
