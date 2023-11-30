using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Fire Blast Perk")]
public class PyroStrikePerk : BasePerk
{
    public override void ApplyPerk()
    {
        Debug.Log("PyroStrikePerk");
    }
}