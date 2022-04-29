using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public interface ICard 
{
    string Title { get; }
    string Statement { get; }
    Sprite Icon { get; }
    List<StatModifier> PositiveModifiers { get; }
    List<StatModifier> NegativeModifiers { get; }
    bool IsTutorial { get; }
}

[System.Serializable]
public class StatModifier
{
    public StatID StatID;
    public float Value;
}