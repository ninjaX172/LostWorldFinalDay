using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerPlacer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerContainer;

    public GameObject playerInstance;


    public void PlacePlayer(Vector2Int startPosition)
    {
        playerInstance = Instantiate(playerPrefab);
        playerInstance.transform.localPosition = new Vector3(startPosition.x + 0.5f, startPosition.y - 0.5f, -2f);
        playerInstance.transform.parent = playerContainer.transform;
    }

    public void TeleportPlayer(Vector2Int position)
    {
        playerInstance.transform.localPosition = new Vector3(position.x, position.y, 0f);
    }

}
