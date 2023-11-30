using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomTypeSpawnWeightParamaters
{
    public RoomType roomType;
    public int intialSpawnWeight;
    public bool pityEnabled;
}



public class RoomTypeProbabilityCalculator : MonoBehaviour
{

    [SerializeField]
    private List<RoomTypeSpawnWeightParamaters> roomTypeSpawnWeightsParameters;
    private WeightedRandomSelector<RoomType> _roomTypeWeightedRandomSelector;

    private Dictionary<RoomType, int> _numberOfTimesSinceLastSpawn;

    //public RoomTypeProbabilityCalculator(List<RoomTypeSpawnWeightParamaters> roomTypeSpawnWeightsParameters)
    //{
    //    this.roomTypeSpawnWeightsParameters = roomTypeSpawnWeightsParameters;
    //}

    void Start()
    {
        _roomTypeWeightedRandomSelector = new WeightedRandomSelector<RoomType>();
        _numberOfTimesSinceLastSpawn = new Dictionary<RoomType, int>();

        if (roomTypeSpawnWeightsParameters.Count == 0)
        {
            Debug.LogError("No room type spawn weights set.");
        }
        foreach (var r in roomTypeSpawnWeightsParameters)
        {
            _roomTypeWeightedRandomSelector.Add(r.roomType, r.intialSpawnWeight);
            _numberOfTimesSinceLastSpawn.Add(r.roomType, 0);
        }
    }

    public RoomType GetRandomRoomType()
    {
        bool toPopRoomTypePortal = GameManager.Instance.GetNumberofBossDefeated() == GameManager.Instance.GetMaxBosses() && _roomTypeWeightedRandomSelector.Contains(RoomType.Portal);
        if (toPopRoomTypePortal)
        {
            _roomTypeWeightedRandomSelector.Remove(RoomType.Portal);
            _numberOfTimesSinceLastSpawn.Remove(RoomType.Portal);
        }
        var roomType = _roomTypeWeightedRandomSelector.GetRandomWeightedItem();
        _numberOfTimesSinceLastSpawn[roomType] = 0;
        foreach (var t in roomTypeSpawnWeightsParameters)
        {
            if (!t.pityEnabled) continue;
            _numberOfTimesSinceLastSpawn[t.roomType] += 1;
            var currentWeight = _roomTypeWeightedRandomSelector.GetObjectWeight(t.roomType);
            var newWeight = currentWeight + _numberOfTimesSinceLastSpawn[t.roomType];
            _roomTypeWeightedRandomSelector.Update(t.roomType, newWeight);
        }

        return roomType;
    }



}
