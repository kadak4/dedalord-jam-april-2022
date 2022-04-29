using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCard : MonoBehaviour, ICard
{
    public string Title => title;

    public string Statement => statement;

    public Sprite Icon => icon;

    public List<StatModifier> PositiveModifiers => positiveModifiers;

    public List<StatModifier> NegativeModifiers => negativeModifiers;

    public bool IsTutorial => isTimed;
    public CardGroup CardGroup { get; }


    [TextArea] [SerializeField] private string title;
    [TextArea] [SerializeField] private string statement;
    [SerializeField] private Sprite icon;

    [SerializeField] private List<StatModifier> positiveModifiers;
    [SerializeField] private List<StatModifier> negativeModifiers;

    [SerializeField] private bool isTimed;

}
