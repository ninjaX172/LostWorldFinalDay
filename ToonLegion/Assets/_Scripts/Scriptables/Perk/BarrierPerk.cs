using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Barrier Perk")]

public class BarrierPerk : BasePerk
{
    // Start is called before the first frame update
    public override void ApplyPerk()
    {
        //Activate script
        GameObject x = GameObject.Find("Player 1 1(Clone)").transform.Find("CounterAbility").gameObject;

        if (x != null)
        {

            x.SetActive(true);
        }
        Debug.Log("Counter");
    }
}
