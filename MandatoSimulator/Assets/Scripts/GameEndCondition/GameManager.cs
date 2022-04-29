using System;
using Padoru.Core;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    [SerializeField] private float minPatrimonyToWin = 50;
    [SerializeField, TextArea] private string winText;
    [SerializeField, TextArea] private string looseInsufficientPatrimonyText;
    [SerializeField, TextArea] private string looseLowStatText;
    [SerializeField, TextArea] private string looseHightStatText;
    
    public event Action<string> OnGameLost;
    public event Action<string> OnGameWon;

    private IDecitionManager decitionManager;
    private IStatsManager statsManager;

    private void Awake()
    {
        Locator.RegisterService<IGameManager>(this);
    }

    private void Start()
    {
        decitionManager = Locator.GetService<IDecitionManager>();
        statsManager = Locator.GetService<IStatsManager>();

        decitionManager.OnFinishedDecitions += OnFinishedDecitions;
        var stats = statsManager.GetAllStats();
        foreach (var stat in stats)
        {
            stat.OnStatValueChanged += OnStatValueChanged;
        }
    }

    private void OnDestroy()
    {
        Locator.UnregisterService<IGameManager>();
        
        
    }

    private void UnsubscribeEverything()
    {
        decitionManager.OnFinishedDecitions -= OnFinishedDecitions;
        var stats = statsManager.GetAllStats();
        foreach (var stat in stats)
        {
            stat.OnStatValueChanged -= OnStatValueChanged;
        }
    }

    private void OnFinishedDecitions()
    {
        var patrimony = statsManager.GetStat(StatID.Patrimonio);

        if (patrimony.Value >= minPatrimonyToWin)
        {
            Win(winText);
        }
        else
        {
            Loose(looseInsufficientPatrimonyText);
        }
    }

    private void OnStatValueChanged(StatID statId)
    {
        var stat = statsManager.GetStat(statId);
        if (stat.Value <= 0)
        {
            Loose(string.Format(looseLowStatText, statId.ToString()));
        }

        if (stat.Value >= stat.MaxValue && statId != StatID.Patrimonio)
        {
            Loose(string.Format(looseHightStatText, statId.ToString()));
        }
    }

    private void Win(string winText)
    {
        UnsubscribeEverything();
        OnGameWon?.Invoke(winText);

    }

    private void Loose(string looseText)
    {
        UnsubscribeEverything();
        OnGameLost?.Invoke(looseText);
    }
}
