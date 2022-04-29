using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsManager
{
    List<IStat> GetAllStats();
    IStat GetStat(StatID id);
    void ApplyModifiers(List<StatModifier> modifiers);
}
