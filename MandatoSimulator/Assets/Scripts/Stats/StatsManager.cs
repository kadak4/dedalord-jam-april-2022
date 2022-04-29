using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;

public class StatsManager : MonoBehaviour, IStatsManager
{
    [SerializeField] private List<ScriptableStat> stats;

    private void Awake()
    {
        // Clone the stats so we don't modify their values at runtime
        for (int i = 0; i < stats.Count; i++)
        {
            var clone = Instantiate(stats[i]);
            stats[i] = clone;
            stats[i].Value = stats[i].InitialValue;
        }
        
        Locator.RegisterService<IStatsManager>(this);
    }

    private void OnDestroy()
    {
        Locator.UnregisterService<IStatsManager>();
    }

    public List<IStat> GetAllStats()
    {
        var genericStats = new List<IStat>();

        foreach (var stat in stats)
        {
            genericStats.Add(stat);
        }
        
        return genericStats;
    }

    public IStat GetStat(StatID id)
    {
        return stats.Find(s => s.Id == id);
    }

    public void ApplyModifiers(List<StatModifier> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            var stat = GetStat(modifier.StatID);
            stat.Value += modifier.Value;
        }
    }
}
