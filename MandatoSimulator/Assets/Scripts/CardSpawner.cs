﻿using System;
using System.Collections;
using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public event Action<ICard> OnCardChanged;

    private ICard currentCard;
    private ICardManager cardManager;
    private IStatsManager statsManager;
    private IDecitionManager decitionManager;

    private void Awake()
    {
        Locator.RegisterService(this);
    }

    void Start()
    {
        cardManager = Locator.GetService<ICardManager>();
        statsManager = Locator.GetService<IStatsManager>();
        decitionManager = Locator.GetService<IDecitionManager>();
        decitionManager.OnDecitionMade += DecitionMade;
        Invoke(nameof(DecitionMade),0.2f);
    }

    private void OnDestroy()
    {
        Locator.UnregisterService<CardSpawner>();
        decitionManager.OnDecitionMade -= DecitionMade;
    }

    public void DecitionMade()
    {
        currentCard = cardManager.GetCard(statsManager.GetAllStats());
        if (currentCard == null)
        {
            Debug.LogError("Card is null");
            return;
        }
        OnCardChanged?.Invoke(currentCard);
    }
}
