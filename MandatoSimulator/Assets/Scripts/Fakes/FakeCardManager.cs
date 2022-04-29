using System.Collections;
using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;

public class FakeCardManager : MonoBehaviour , ICardManager
{
    public FakeCard[] cards;

    public ICard GetCard(List<IStat> currentStats)
    {
        return cards[Random.Range(0,cards.Length)];
    }

    private void Awake()
    {
        Locator.RegisterService<ICardManager>(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
