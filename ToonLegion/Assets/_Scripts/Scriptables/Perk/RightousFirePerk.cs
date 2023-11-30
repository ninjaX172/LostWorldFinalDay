using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/RightousFire Perk")]
public class RightousFirePerk : BasePerk
{
    public override void ApplyPerk()
    {
        //Activate rightous fire
        GameObject fireaura = GameObject.Find("Player 1 1(Clone)").transform.Find("RighteousFireNewV").gameObject;

        if (fireaura != null)
        {
            fireaura.SetActive(true);
        }

        Debug.Log("Fire Perk");    

    }
}