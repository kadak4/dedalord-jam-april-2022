using Padoru.Core;
using UnityEngine;

public class GetCardTest : MonoBehaviour
{
    [SerializeField] private KeyCode getCardKey = KeyCode.G;
    
    private ICardManager cardsManager;
    private IStatsManager statsManager;
    
    void Start()
    {
        cardsManager = Locator.GetService<ICardManager>();
        statsManager = Locator.GetService<IStatsManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(getCardKey))
        {
            var card = GetNextCard();
            Debug.Log(card.Title);
        }
    }

    private ICard GetNextCard()
    {
        return cardsManager.GetCard(statsManager.GetAllStats());
    }
}
