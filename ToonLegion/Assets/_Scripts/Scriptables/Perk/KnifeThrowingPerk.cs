using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Knife Throw Perk")]
public class KnifeThrowingPerk : BasePerk
{
    public override void ApplyPerk()
    {
        GameObject knife = GameObject.Find("Player 1 1(Clone)").transform.Find("Sword Controller").gameObject;

        if (knife != null)
        {
            knife.SetActive(true);
        }
        //Activate apply proj script
        Debug.Log("knife activated");
    }
}