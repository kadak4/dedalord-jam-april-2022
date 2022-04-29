using System.Collections;
using System.Collections.Generic;
using Padoru.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI statement;
    public Image image;
    public List<GameObject> iconReferences;
    public AudioClip wobbleClip;

    private Dictionary<StatID, GameObject> statIcons;
    private CardSpawner cardSpawner;
    private IDecitionManager decitionManager;
    private IStatsManager statsManager;
    private AudioHandler audioHandler;
    private List<IStat> stats;

    private void Start()
    {
        statsManager = Locator.GetService<IStatsManager>();
        cardSpawner = Locator.GetService<CardSpawner>();
        cardSpawner.OnCardChanged += SetCard;
        decitionManager = Locator.GetService<IDecitionManager>();
        audioHandler = Locator.GetService<AudioHandler>();
        stats = statsManager.GetAllStats();
        statIcons = new Dictionary<StatID, GameObject>();
        for (int i = 0; i < iconReferences.Count; i++)
        {
            statIcons.Add(stats[i].Id, iconReferences[i]);
        }
    }

    private void OnDestroy()
    {
        cardSpawner.OnCardChanged -= SetCard;
    }

    public void PlayWobbleSound()
    {
        audioHandler.PlayAudioClip(wobbleClip);
    }

    public void SetCard(ICard card)
    {
        title.text = card.Title;
        statement.text = card.Statement;
        image.sprite = card.Icon;
        foreach (var item in stats)
        {
            statIcons[item.Id].SetActive(false);
        }
        foreach (var item in card.PositiveModifiers)
        {
            statIcons[item.StatID].SetActive(true);
        }
        foreach (var item in card.NegativeModifiers)
        {
            statIcons[item.StatID].SetActive(true);
        }
    }

    public void OnCardFell()
    {
        decitionManager.DidNotPick();

    }
}
