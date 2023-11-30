using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PropParameters
{
    public PropType propType;
    public GameObject propPrefab;
    public int maxProps;
    public int weight;
}
public class PropSpawner : MonoBehaviour
{
    [SerializeField]
    private List<PropParameters> propParameters;
    [SerializeField]
    private GameObject propContainer;
    private WeightedRandomSelector<PropParameters> propSelector = new WeightedRandomSelector<PropParameters>();
    private Dictionary<PropType, List<GameObject>> propDictionary = new Dictionary<PropType, List<GameObject>>();

    [SerializeField]
    private GameObject specialCaseBossLeavePortalPrefab;

    private void OnEnable()
    {
        EventManager.Instance.BossDeath += EventManager_OnBossDeath;


    }

    private void OnDisable()
    {
        EventManager.Instance.BossDeath -= EventManager_OnBossDeath;

    }

    private void EventManager_OnBossDeath(string bossName, Vector2Int bossDeathPosition)
    {
        var prop = Instantiate(specialCaseBossLeavePortalPrefab);
        prop.transform.localPosition = new Vector3(bossDeathPosition.x - 0.5f, bossDeathPosition.y - 1f, -2f);
        prop.transform.parent = propContainer.transform;
    }



    void Start()
    {
        // propSelector.AddMany(propParameters, propParameters.ConvertAll(x => x.weight));
        foreach (var p in propParameters)
        {
            if (!propDictionary.ContainsKey(p.propType))
                propDictionary.Add(p.propType, new List<GameObject>());
            propDictionary[p.propType].Add(p.propPrefab);

            if (p.propType is PropType.Decoration or PropType.Destroyable or PropType.Obstacle)
            {
                propSelector.Add(p, p.weight);

            }
        }
    }

    public HashSet<Vector2Int> SpawnObstacleDecorationPropsRandomly(HashSet<Vector2Int> validSpawnPoints, int minProps, int maxProps)
    {
        var positionsUsedForProps = new HashSet<Vector2Int>();
        var numOfPropsToSpawn = Random.Range(minProps, maxProps);

        var validSpawnPointsList = new List<Vector2Int>(validSpawnPoints);

        for (int i = 0; i < numOfPropsToSpawn; i++)
        {
            var propParameters = propSelector.GetRandomWeightedItem();
            var numToSpawn = Random.Range(1, propParameters.maxProps + 1);

            for (int j = 0; j < numToSpawn; j++)
            {
                if (validSpawnPointsList.Count == 0 || i >= numOfPropsToSpawn)
                {
                    break;
                }
                var position = validSpawnPointsList[Random.Range(0, validSpawnPointsList.Count)];
                validSpawnPoints.Remove(position);
                var prop = Instantiate(propParameters.propPrefab);
                prop.transform.localPosition = new Vector3(position.x, position.y, -2f);
                prop.transform.parent = propContainer.transform;
                positionsUsedForProps.Add(position);
                i += 1;
            }
        }
        return positionsUsedForProps;
    }

    public List<GameObject> SpawnBulletPropsRandomly(HashSet<Vector2Int> validSpawnPoints, int minNum, int maxNum)
    {
        var props = new List<GameObject>();
        var positionsUsedForProps = new HashSet<Vector2Int>();
        var numOfPropsToSpawn = Random.Range(minNum, maxNum);
        var validSpawnPointsList = new List<Vector2Int>(validSpawnPoints);
        for (int i = 0; i < numOfPropsToSpawn; i++)
        {
            var randomProp = propDictionary[PropType.Bullet][Random.Range(0, propDictionary[PropType.Bullet].Count)];
            var position = validSpawnPointsList[Random.Range(0, validSpawnPointsList.Count)];
            validSpawnPoints.Remove(position);
            var prop = Instantiate(randomProp);
            prop.transform.localPosition = new Vector3(position.x, position.y, -2f);
            prop.transform.parent = propContainer.transform;
            // positionsUsedForProps.Add(position);
            props.Add(prop);
        }

        return props;
    }

    public GameObject PlaceAPropByPropType(Vector2Int position, PropType propType)
    {

        var randomProp = propDictionary[propType][Random.Range(0, propDictionary[propType].Count)];
        var prop = Instantiate(randomProp);
        prop.transform.localPosition = new Vector3(position.x - 0.5f, position.y - 1f, -2f);
        prop.transform.parent = propContainer.transform;

        // return new HashSet<Vector2Int>(){position};
        return prop;
    }



}
