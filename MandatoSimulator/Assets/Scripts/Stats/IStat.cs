using UnityEngine;
using System;

public interface IStat 
{
    StatID Id { get;}
    float InitialValue { get;}
    float MaxValue { get;}
    float Value { get; set; }
    string Name { get; }
    string Description { get; }
    Sprite Icon { get; }
    event Action<StatID> OnStatValueChanged;
}
