using System;
using System.Collections;
using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;
using UnityEngine.UI;

public class DecitionManager : MonoBehaviour, IDecitionManager
{
    public event Action OnDecitionMade;
    public event Action OnFinishedDecitions;

    public int TotalDecitions = 48;
    public Slider TimelineSlider;

    private CardSpawner cardSpawner;
    private IStatsManager statsManager;
    private ICard currentCard;
    private int decitionsMade = 0;

    private void Awake()
    {
        Locator.RegisterService(this);
    }

    void Start()
    {
        statsManager = Locator.GetService<IStatsManager>();
        cardSpawner = Locator.GetService<CardSpawner>();
        cardSpawner.OnCardChanged += CardChanged;
    }

    private void OnDestroy()
    {
        Locator.UnregisterService<DecitionManager>();
        cardSpawner.OnCardChanged -= CardChanged;
    }

    public void PickedYes()
    {
        UpdateUI(currentCard);
        statsManager.ApplyModifiers(currentCard.PositiveModifiers);
        OnDecitionMade?.Invoke();
    }

    public void PickedNo()
    {
        UpdateUI(currentCard);
        statsManager.ApplyModifiers(currentCard.NegativeModifiers);
        OnDecitionMade?.Invoke();
    }

    private void UpdateUI(ICard card)
    {
        if (!card.IsTutorial)
        {
            return;
        }
        decitionsMade++;
        TimelineSlider.value = (float)decitionsMade / (float)TotalDecitions;
        if (decitionsMade >= TotalDecitions)
        {
            OnFinishedDecitions?.Invoke();
        }
    }

    private void CardChanged(ICard newCard)
    {
        currentCard = newCard;
    }
}
