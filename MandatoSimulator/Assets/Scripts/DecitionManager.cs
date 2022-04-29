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
    public float TimeForCards = 15;
    public Animator CardAnimator;
    public List<StatModifier> FailedToPickModifiers;

    private CardSpawner cardSpawner;
    private IStatsManager statsManager;
    private ICard currentCard;
    private int decitionsMade = 0;
    private float timeLeft = -1;

    private void Awake()
    {
        Locator.RegisterService<IDecitionManager>(this);
    }

    void Start()
    {
        statsManager = Locator.GetService<IStatsManager>();
        cardSpawner = Locator.GetService<CardSpawner>();
        cardSpawner.OnCardChanged += CardChanged;
    }

    private void OnDestroy()
    {
        Locator.UnregisterService<IDecitionManager>();
        cardSpawner.OnCardChanged -= CardChanged;
    }

    void FixedUpdate()
    {
        if (timeLeft != -1)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                CardAnimator.SetTrigger("Fell");
                timeLeft = -1;
            }else if (timeLeft < 3)
            {
                CardAnimator.SetBool("IsTrembling", true);
            }
        }
    }

    public void DidNotPick()
    {
        UpdateUI(currentCard);
        statsManager.ApplyModifiers(FailedToPickModifiers);
        OnDecitionMade?.Invoke();
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
        CardAnimator.SetBool("IsTrembling", false);
        if (card == null)
        {
            return;
        }
        if (card.IsTutorial)
        {
            timeLeft = -1;
            return;
        }
        timeLeft = TimeForCards;
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
