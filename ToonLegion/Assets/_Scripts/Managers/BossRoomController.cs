

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossRoomParameters
{
    public GameObject BossRoomPrefab;
    public Vector2Int SpawnPositionOffset = new Vector2Int(0, 0);

}
public class BossRoomController : MonoBehaviour
{
    [SerializeField]
    public List<BossRoomParameters> bossRoomParameters;

    public BossRoomParameters activeBossRoomParameters;
    public GameObject activeRoomObject;

    public void InstantiateRandomBossRoom(Vector2Int position)
    {
        activeBossRoomParameters = bossRoomParameters[Random.Range(0, bossRoomParameters.Count)];

        activeRoomObject = Instantiate(activeBossRoomParameters.BossRoomPrefab);
        activeRoomObject.transform.localPosition = new Vector3(position.x, position.y, -2f);
    }

    public Vector2 GetPlayerSpawnPoint()
    {
        var offset = activeBossRoomParameters.SpawnPositionOffset;
        return new Vector2(activeRoomObject.transform.position.x + offset.x,
            activeRoomObject.transform.position.y + offset.y);
    }

    public void DestroyBossRoom()
    {
        if (activeRoomObject != null)
        {
            bossRoomParameters.Remove(activeBossRoomParameters);
            Debug.Log(bossRoomParameters);

            Destroy(activeRoomObject);
            

        }
    }



}