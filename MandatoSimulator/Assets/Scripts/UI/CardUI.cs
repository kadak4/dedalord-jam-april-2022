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

    private CardSpawner cardSpawner;
    private IDecitionManager decitionManager;

    private void Start()
    {
        cardSpawner = Locator.GetService<CardSpawner>();
        cardSpawner.OnCardChanged += SetCard;
        decitionManager = Locator.GetService<IDecitionManager>();
    }

    private void OnDestroy()
    {
        cardSpawner.OnCardChanged -= SetCard;
    }

    public void SetCard(ICard card)
    {
        title.text = card.Title;
        statement.text = card.Statement;
        image.sprite = card.Icon;
    }

    public void OnCardFell()
    {
        decitionManager.DidNotPick();

    }
}
