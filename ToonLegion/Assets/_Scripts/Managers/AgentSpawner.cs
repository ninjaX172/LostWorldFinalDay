using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;



class DifficultyController
{
    private PercentageRandomSelector<Difficulty> difficultyWeightedRandomSelector = new();
    public readonly int MinNumberOfEnemies;
    public readonly int MaxNumberOfEnemies;

    public DifficultyController(List<float> enemyProbability, int minNumberOfEnemies, int maxNumberOfEnemies)
    {
        var difficultValues = Enum.GetValues(typeof(Difficulty))
            .Cast<Difficulty>()
            .ToList();

        MinNumberOfEnemies = minNumberOfEnemies;
        MaxNumberOfEnemies = maxNumberOfEnemies;

        difficultyWeightedRandomSelector = new PercentageRandomSelector<Difficulty>();
        difficultyWeightedRandomSelector.AddMany(difficultValues, enemyProbability);
    }

    public List<Difficulty> GetRandomChoices()
    {
        var numberOfEnemies = Random.Range(MinNumberOfEnemies, MaxNumberOfEnemies);
        var difficulties = new List<Difficulty>();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var difficulty = difficultyWeightedRandomSelector.GetRandomChoice();
            difficulties.Add(difficulty);
        }
        return difficulties;
    }
}

public class AgentSpawner : MonoBehaviour
{
    [SerializeField]
    private List<EnemyPrefabAndDifficulty> enemyPrefabsAndDifficulty;
    [SerializeField]
    private List<DifficultyParameters> difficultyParameters;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField] private GameObject _miniMinionPrefab;

    private readonly Dictionary<Difficulty, DifficultyController> _difficultyControllers = new();
    private readonly Dictionary<Difficulty, List<GameObject>> _enemyPrefabsByDifficulty = new();

    private readonly Dictionary<Difficulty, List<float>> _enemyProbabilityByDifficulty = new()
    {
        { Difficulty.Easy, new List<float> { 0.75f, 0.15f, 0.05f, 0.05f } },
        { Difficulty.Medium, new List<float> { 0.35f, 0.3f, 0.2f, 0.15f } },
        { Difficulty.Hard, new List<float> { 0.2f, 0.25f, 0.3f, 0.25f } },
        { Difficulty.Hell, new List<float> { 0.05f, 0.2f, 0.4f, 0.35f } }
    };

    void Start()
    {
        if (difficultyParameters.Count != 4)
        {
            Debug.LogError("There must be 4 difficulty parameters.");
            return;
        }

        var difficultyValues = Enum.GetValues(typeof(Difficulty))
            .Cast<Difficulty>()
            .ToList();

        foreach (var difficultyValue in difficultyValues)
        {
            var difficultyController = new DifficultyController(_enemyProbabilityByDifficulty[difficultyValue],
                difficultyParameters[(int)difficultyValue].minEnemies,
                difficultyParameters[(int)difficultyValue].maxEnemies);
            _difficultyControllers.Add(difficultyValue, difficultyController);
        }

        foreach (var enemyPrefabAndDifficulty in enemyPrefabsAndDifficulty)
        {
            var k = enemyPrefabAndDifficulty.difficulty;
            var v = enemyPrefabAndDifficulty.enemyPrefab;
            if (!_enemyPrefabsByDifficulty.ContainsKey(k))
            {
                _enemyPrefabsByDifficulty.Add(k, new List<GameObject>());
            }
            _enemyPrefabsByDifficulty[k].Add(v);
        }
    }

    private List<GameObject> GetListOfEnemiesGivenDifficulty(Difficulty difficulty)
    {
        var enemyPrefabs = new List<GameObject>();
        var listOfDifficulties = _difficultyControllers[difficulty].GetRandomChoices();

        foreach (var d in listOfDifficulties)
        {
            if (_enemyPrefabsByDifficulty[d].Count == 0)
            {
                Debug.LogError("No enemies for difficulty " + d);
                continue;
            }
            var randomInt = Random.Range(0, _enemyPrefabsByDifficulty[d].Count);
            var enemyPrefab = _enemyPrefabsByDifficulty[d][randomInt];
            enemyPrefabs.Add(enemyPrefab);
        }
        return enemyPrefabs;
    }

    public List<GameObject> SpawnEnemiesGivenPossiblePositions(List<Vector2Int> validSpawnPoints, Difficulty difficulty)
    {
        var enemyPrefabs = GetListOfEnemiesGivenDifficulty(difficulty);
        var listOfEnemiesReferences = new List<GameObject>();

        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            var enemyPrefab = enemyPrefabs[i];
            var enemy = Instantiate(enemyPrefab);

            var randomValidSpawnPoint = validSpawnPoints[Random.Range(0, validSpawnPoints.Count)];
            validSpawnPoints.Remove(randomValidSpawnPoint);
            enemy.transform.position = new Vector3(randomValidSpawnPoint.x, randomValidSpawnPoint.y, -2f);
            enemy.transform.parent = _enemyContainer.transform;
            enemy.SetActive(false);
            listOfEnemiesReferences.Add(enemy);
        }

        return listOfEnemiesReferences;
    }

    public List<GameObject> SpawnMiniMinionsGivenPossiblePositions(List<Vector2Int> validSpawnPoints, int numberOfMinions)
    {
        var listOfMinionsReferences = new List<GameObject>();

        for (int i = 0; i < numberOfMinions; i++)
        {
            var minion = Instantiate(_miniMinionPrefab);

            var randomValidSpawnPoint = validSpawnPoints[Random.Range(0, validSpawnPoints.Count)];
            validSpawnPoints.Remove(randomValidSpawnPoint);
            minion.transform.position = new Vector3(randomValidSpawnPoint.x, randomValidSpawnPoint.y, -2f);
            minion.transform.parent = _enemyContainer.transform;
            minion.SetActive(false);
            listOfMinionsReferences.Add(minion);
        }

        return listOfMinionsReferences;
    }




}


[System.Serializable]
public class EnemyPrefabAndDifficulty
{
    public Difficulty difficulty;
    public GameObject enemyPrefab;
}

[System.Serializable]
public class DifficultyParameters
{
    public Difficulty difficulty;
    public int minEnemies;
    public int maxEnemies;
}
