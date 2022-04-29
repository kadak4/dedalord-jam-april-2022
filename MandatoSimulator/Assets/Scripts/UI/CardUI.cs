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

    private void Start()
    {
        cardSpawner = Locator.GetService<CardSpawner>();
        cardSpawner.OnCardChanged += SetCard;
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
}
