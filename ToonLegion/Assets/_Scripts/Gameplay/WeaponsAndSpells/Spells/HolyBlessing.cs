using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBlessing : MonoBehaviour
{
    
    private void Start()
    {
        PlayerStats ps = this.transform.parent.GetComponent<PlayerStats>();
        ps.currentMaxHealth += 1;
        ps.currentMoveSpeed += 2;
        
    }

}
 