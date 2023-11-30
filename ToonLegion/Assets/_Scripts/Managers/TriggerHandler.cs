using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public Vector2Int RoomId;
    public Vector2Int DoorDirection;


    private void OnTriggerEnter2D(Collider2D other)
    {
        // This method is called when a collider enters the trigger.
        // You can add your custom logic here to respond to the trigger event.
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player triggered from room " + RoomId + " in direction " + DoorDirection);

            EventManager.Instance.OnPlayerEnterDoor(RoomId, DoorDirection);
        }
    }



}