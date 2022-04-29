using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Card")]
public class ScriptableCard : ScriptableObject, ICard
{
    [SerializeField] private string title;
    [SerializeField, TextArea] private string statement;
    [SerializeField] private Sprite icon;
    [SerializeField] private bool isTutorial;
    [SerializeField] private CardGroup cardGroup;
    [SerializeField] private List<StatModifier> positiveModifiers;
    [SerializeField] private List<StatModifier> negativeModifiers;

    public string Title => title;
    public string Statement => statement;
    public Sprite Icon => icon;
    public List<StatModifier> PositiveModifiers => positiveModifiers;
    public List<StatModifier> NegativeModifiers => negativeModifiers;
    public bool IsTutorial => isTutorial;
    public CardGroup CardGroup => cardGroup;
}
