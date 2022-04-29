using System;
using System.Collections;
using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;

public class DecitionManager : MonoBehaviour
{
    public event Action OnDecitionMade;

    private CardSpawner cardSpawner;
    private IStatsManager statsManager;
    private ICard currentCard;

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
        cardSpawner.OnCardChanged -= CardChanged;
    }

    public void PickedYes()
    {
        statsManager.ApplyModifiers(currentCard.PositiveModifiers);
        OnDecitionMade?.Invoke();
    }

    public void PickedNo()
    {
        statsManager.ApplyModifiers(currentCard.NegativeModifiers);
        OnDecitionMade?.Invoke();
    }

    private void CardChanged(ICard newCard)
    {
        currentCard = newCard;
    }
}
