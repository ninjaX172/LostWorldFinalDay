

using UnityEngine;

public class PortalToLeaveBossRoomTriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // This method is called when a collider enters the trigger.
        // You can add your custom logic here to respond to the trigger event.
        if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Player triggered from room " + RoomId + " in direction " + DoorDirection);
            Debug.Log("Player triggered portal to leave");
            EventManager.Instance.OnLeaveBossRoom();

            Destroy(gameObject);
        }
    }
}