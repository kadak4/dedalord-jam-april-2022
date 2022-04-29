using System;
using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardsManager : MonoBehaviour, ICardManager
{
    [SerializeField] private List<ScriptableCard> cards;

    private List<ScriptableCard> groupOneCards;
    private List<ScriptableCard> groupTwoCards;
    private List<ScriptableCard> groupThreeCards;
    
    private int groupIndex;
    
    private void Awake()
    {
        SetupGroups();

        Locator.RegisterService<ICardManager>(this);
    }

    private void OnDestroy()
    {
        Locator.UnregisterService<ICardManager>();
    }

    private void SetupGroups()
    {
        groupOneCards = new List<ScriptableCard>();
        groupTwoCards = new List<ScriptableCard>();
        groupThreeCards = new List<ScriptableCard>();

        // Clone the cards so we don't modify their values at runtime
        for (int i = 0; i < cards.Count; i++)
        {
            var clone = Instantiate(cards[i]);
            if (clone.CardGroup == CardGroup.Grupo1)
            {
                groupOneCards.Add(clone);
            }
            else if (clone.CardGroup == CardGroup.Grupo2)
            {
                groupTwoCards.Add(clone);
            }
            else if (clone.CardGroup == CardGroup.Grupo3)
            {
                groupThreeCards.Add(clone);
            }
        }
    }

    public ICard GetCard(List<IStat> currentStats)
    {
        var index = groupIndex;
        groupIndex = (groupIndex + 1) % Enum.GetNames(typeof(CardGroup)).Length;
        return GetRandomCardFrom((CardGroup)index);
    }

    private ICard GetRandomCardFrom(CardGroup group)
    {
        if (group == CardGroup.Grupo1)
        {
            return GetRandomCardFrom(groupOneCards);
        } 
        if (group == CardGroup.Grupo2)
        {
            return GetRandomCardFrom(groupTwoCards);
        }
        if (group == CardGroup.Grupo3)
        {
            return GetRandomCardFrom(groupThreeCards);
        }

        return null;
    }
    
    private ICard GetRandomCardFrom(List<ScriptableCard> list)
    {
        if (list.Count <= 0)
        {
            return null;
        }
        
        var index = Random.Range(0, list.Count);
        var card = list[index];
        list.RemoveAt(index);
        return card;
    }
}
