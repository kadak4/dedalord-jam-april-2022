using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard 
{
    string Title { get; }
    string Statement { get; }
    Sprite Icon { get; }
    List<StatModifier> PositiveModifiers { get; }
    List<StatModifier> NegativeModifiers { get; }
    bool IsTimed { get; }
}

public class StatModifier
{
    public StatID ID;
    public float Value;
}