using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameTextUI : MonoBehaviour
{
    private TextMeshProUGUI _gameTimeText;
    private Difficulty _currentDifficulty;
    private TextMeshProUGUI _gameDifficultyText;
    private TextMeshProUGUI _numOfSoulsText;
    private TextMeshProUGUI _reaperIsNearText;
    private float colorChangeDuration;
    private HashSet<string> _uniqueBossesKilled = new HashSet<string>();


    [SerializeField]
    private Color startColor;
    [SerializeField]
    private Color endColor;

    [SerializeField]
    private float mediumDifficultyTime = 30f;
    [SerializeField]
    private float hardDifficultyTime = 60f;
    [SerializeField]
    private float hellDifficultyTime = 90f;

    [SerializeField] private Color easyColor;
    [SerializeField] private Color mediumColor;
    [SerializeField] private Color hardColor;
    [SerializeField] private Color hellColor;

    void Awake()
    {
        _gameTimeText = GameObject.Find("Timer Panel").transform.Find("Timer Text").GetComponent<TextMeshProUGUI>();
        _gameDifficultyText = GameObject.Find("Difficulty Panel").transform.Find("Difficulty Text").GetComponent<TextMeshProUGUI>();
        _numOfSoulsText = GameObject.Find("Soul Panel").transform.Find("Soul Text").GetComponent<TextMeshProUGUI>();
        _reaperIsNearText = GameObject.Find("Reaper Panel").transform.Find("Reaper Text").GetComponent<TextMeshProUGUI>();
        _reaperIsNearText.enabled = false;
        _currentDifficulty = Difficulty.Easy;
        colorChangeDuration = hellDifficultyTime;
    }

    private void OnEnable()
    {
        EventManager.Instance.BossDeath += EventManager_OnBossDeath;
        EventManager.Instance.PlayerEnteredReaperRadius += EventManager_OnPlayerEnteredReaperRadius;
        EventManager.Instance.PlayerLeaveReaperRadius += EventManager_OnPlayerLeaveReaperRadius;
    }

    private void EventManager_OnPlayerLeaveReaperRadius()
    {
        _reaperIsNearText.enabled = false;
    }

    private void EventManager_OnPlayerEnteredReaperRadius()
    {
        _reaperIsNearText.enabled = true;
    }

    private void EventManager_OnBossDeath(string arg0, Vector2Int arg1)
    {
        _uniqueBossesKilled.Add(arg0);
    }

    private void OnDisable()
    {
        EventManager.Instance.BossDeath -= EventManager_OnBossDeath;
        EventManager.Instance.PlayerEnteredReaperRadius -= EventManager_OnPlayerEnteredReaperRadius;

    }

    void Update()
    {
        var elapsedTime = Time.timeSinceLevelLoad;
        var minutes = (int)elapsedTime / 60;
        var seconds = (int)elapsedTime % 60;
        var milliseconds = (int)(elapsedTime * 100) % 100;

        var difficulty = GetDifficulty(elapsedTime);


        _gameTimeText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
        _gameDifficultyText.text = getDifficultyString(difficulty);
        _gameDifficultyText.color = difficulty switch
        {
            Difficulty.Easy => easyColor,
            Difficulty.Medium => mediumColor,
            Difficulty.Hard => hardColor,
            Difficulty.Hell => hellColor,
            _ => easyColor
        };

        _numOfSoulsText.text = $"Soul:{GameManager.Instance.GetNumberofBossDefeated()}/{GameManager.Instance.GetMaxBosses()}";
        

        if (elapsedTime < colorChangeDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / colorChangeDuration);
            _gameTimeText.color = Color.Lerp(startColor, endColor, t);
        }


        if (difficulty != _currentDifficulty)
        {
            _currentDifficulty = difficulty;
            EventManager.Instance.OnDifficultyUpdate(difficulty);
        }
    }

    Difficulty GetDifficulty(float elapsedTime)
    {

        Difficulty difficulty;
        if (elapsedTime < mediumDifficultyTime)
        {
            difficulty = Difficulty.Easy;
        }
        else if (elapsedTime < hardDifficultyTime)
        {
            difficulty = Difficulty.Medium;
        }
        else if (elapsedTime < hellDifficultyTime)
        {
            difficulty = Difficulty.Hard;
        }
        else
        {
            difficulty = Difficulty.Hell;
        }
        return difficulty;
    }

    private string getDifficultyString(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Easy => "Easy",
            Difficulty.Medium => "Medium",
            Difficulty.Hard => "Hard",
            Difficulty.Hell => "Hell",
            _ => "Easy"
        }; ;
    }

}
