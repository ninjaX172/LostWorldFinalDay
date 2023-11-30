using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Blessing Perk")]
public class BlessingPerk : BasePerk
{
    public override void ApplyPerk()
    {
        //Activate script
        GameObject Bless = GameObject.Find("Player 1 1(Clone)").transform.Find("Blessing").gameObject;

        if (Bless != null)
        {
            
            Bless.SetActive(true);
        }
        Debug.Log("Blessing of God Perk");    
    }
}