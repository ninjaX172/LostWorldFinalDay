using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/SpeicalFriend Perk")]
public class SpeicalFriendPerk : BasePerk
{
    public override void ApplyPerk()
    {
        //Activate speical friend
        GameObject friend = GameObject.Find("Player 1 1(Clone)").transform.Find("Rotator For Speical").gameObject;

        if(friend != null)
        {
            friend.SetActive(true);
        }
        Debug.Log("Speical Friend Perk");    
    }
}