using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Resources used: https://www.youtube.com/watch?v=Q_FbwhKnues
public class EventManager : Singleton<EventManager>
{

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.Instance.CurrentScreen.screenType == ScreenType.GameplayScreen)
            {
                OnPauseScreenTriggered();
            }
            else
            {
                OnPauseScreenClosed();
            }
        }

    }

    public event UnityAction PerkSelectionScreenTriggered;
    public void OnPerkSelectionScreenTriggered() => PerkSelectionScreenTriggered?.Invoke();

    public event UnityAction PerkSelectionScreenClosed;
    public void OnPerkSelectionScreenClosed() => PerkSelectionScreenClosed?.Invoke();

    public event UnityAction<BasePerk> PerkSelected;
    public void OnPerkSelected(BasePerk perk) => PerkSelected?.Invoke(perk);

    public event UnityAction MightPickedUp;
    public void OnMightPickedUp() => MightPickedUp?.Invoke();

    public event UnityAction<int, int> PlayerHealthChanged;
    public void OnPlayerHealthChange(int currentHealth, int maxHealth) => PlayerHealthChanged?.Invoke(currentHealth, maxHealth);

    public event UnityAction PauseScreenTriggered;
    public void OnPauseScreenTriggered() => PauseScreenTriggered?.Invoke();

    public event UnityAction PauseScreenClosed;
    public void OnPauseScreenClosed() => PauseScreenClosed?.Invoke();

    public event UnityAction<(Vector2Int, Vector2Int)> PlayerEnterDoor;
    public void OnPlayerEnterDoor(Vector2Int roomId, Vector2Int direction) => PlayerEnterDoor?.Invoke((roomId, direction));
    public event UnityAction PortalEntered;
    public void OnPortalEntered() => PortalEntered?.Invoke();

    public event UnityAction LeaveBossRoom;
    public void OnLeaveBossRoom() => LeaveBossRoom?.Invoke();

    public event UnityAction<string, Vector2Int> BossDeath;
    public void OnBossDeath(string bossName, Vector2Int bossDeathPosition) => BossDeath?.Invoke(bossName, bossDeathPosition);

    public event UnityAction<Difficulty> DifficultyUpdate;
    public void OnDifficultyUpdate(Difficulty difficulty) => DifficultyUpdate?.Invoke(difficulty);

    public event UnityAction PlayerEnteredReaperRadius;
    public void OnPlayerEnterReaperRadius() => PlayerEnteredReaperRadius?.Invoke();

    public event UnityAction PlayerLeaveReaperRadius;
    public void OnPlayerLeaveReaperRadius() => PlayerLeaveReaperRadius?.Invoke();

    public event UnityAction PlayerEnteredGrimTrial;
    public void OnPlayerEnteredGrimTrial() => PlayerEnteredGrimTrial?.Invoke();


    public event UnityAction PlayerDefeatedAllBosses;
    public void OnPlayerDefeatedAllBosses() => PlayerDefeatedAllBosses?.Invoke();

    public event UnityAction GrimStunned;
    public void OnGrimStunned() => GrimStunned?.Invoke();

    public event UnityAction GrimUnstunned;
    public void OnGrimUnstunned() => GrimUnstunned?.Invoke();



}

