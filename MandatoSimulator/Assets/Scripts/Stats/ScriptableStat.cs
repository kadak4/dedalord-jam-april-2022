using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Stat")]
public class ScriptableStat : ScriptableObject, IStat
{
    [SerializeField] private new string name; 
    [SerializeField, TextArea] private string description; 
    [SerializeField] private StatID statID; 
    [SerializeField] private float maxValue; 
    [SerializeField] private Sprite icon;

    private float value = 0;
    
    public string Name => name;
    public string Description => description;
    public StatID Id => statID;
    public float MaxValue => maxValue;
    public float Value
    {
        get => value;
        set
        {
            this.value = value;
            OnStatValueChanged?.Invoke(statID);
        }
    }
    public Sprite Icon => icon;

    public event Action<StatID> OnStatValueChanged;
}
