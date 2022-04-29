using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;

public class CardsManager : MonoBehaviour, ICardManager
{
    [SerializeField] private List<ScriptableCard> cards;

    private int cardIndex;
    
    private void Awake()
    {
        Locator.RegisterService<ICardManager>(this);
    }

    private void OnDestroy()
    {
        Locator.UnregisterService<ICardManager>();
    }

    public ICard GetCard(List<IStat> currentStats)
    {
        var index = cardIndex;
        cardIndex = (cardIndex + 1) % cards.Count;
        return cards[index];
    }
}
