using UnityEngine;

[CreateAssetMenu(menuName = "Game/Stat")]
public class ScriptableStat : ScriptableObject, IStat
{
    [SerializeField] private new string name; 
    [SerializeField, TextArea] private string description; 
    [SerializeField] private StatID statID; 
    [SerializeField] private float maxValue; 
    [SerializeField] private Sprite icon;

    public string Name => name;
    public string Description => description;
    public StatID Id => statID;
    public float MaxValue => maxValue;
    public float Value { get; set; }
    public Sprite Icon => icon;
}
