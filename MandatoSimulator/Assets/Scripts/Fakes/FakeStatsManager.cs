using System.Collections;
using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;

public class FakeStatsManager : MonoBehaviour, IStatsManager
{
    public void ApplyModifiers(List<StatModifier> modifiers)
    {

    }

    public List<IStat> GetAllStats()
    {
        return new List<IStat>();
    }

    public IStat GetStat(StatID id)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        Locator.RegisterService<IStatsManager>(this);
    }
}
