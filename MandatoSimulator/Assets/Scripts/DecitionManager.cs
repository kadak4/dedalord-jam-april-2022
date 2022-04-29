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
    public AudioClip PageTurnClip;

    private CardSpawner cardSpawner;
    private IStatsManager statsManager;
    private AudioHandler audioHandler;
    private ICard currentCard;
    private int decitionsMade = 0;
    private float timeLeft = -1, timelineTargetValue;

    private void Awake()
    {
        Locator.RegisterService<IDecitionManager>(this);
    }

    void Start()
    {
        statsManager = Locator.GetService<IStatsManager>();
        cardSpawner = Locator.GetService<CardSpawner>();
        audioHandler = Locator.GetService<AudioHandler>();
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

    private void Update()
    {
        if (timelineTargetValue != TimelineSlider.value)
        {
            var difference = timelineTargetValue - TimelineSlider.value;
            if (Mathf.Abs(difference) < 0.1f * Time.deltaTime)
            {
                TimelineSlider.value = timelineTargetValue;
            }
            else
            {
                TimelineSlider.value = Mathf.Lerp(TimelineSlider.value, timelineTargetValue, 0.1f);
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
        audioHandler.PlayAudioClip(PageTurnClip);
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
        timelineTargetValue = (float)decitionsMade / (float)TotalDecitions;
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
