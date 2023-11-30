using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Fireshield Perk")]
public class FireshieldPerk : BasePerk
{
    public override void ApplyPerk()
    {
        GameObject fireShield = GameObject.Find("Player 1 1(Clone)").transform.Find("FireShield Controller").gameObject;

        if (fireShield != null)
        {
            fireShield.SetActive(true);
        }
    }
}