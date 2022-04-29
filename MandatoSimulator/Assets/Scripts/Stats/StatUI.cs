using System.Collections;
using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    public StatID id;
    public Slider slider;

    private IStatsManager statManager;
    private IStat stat;

    void Start()
    {
        statManager = Locator.GetService<IStatsManager>();
        stat = statManager.GetStat(id);
        stat.OnStatValueChanged += OnValueChanged;
    }

    void OnValueChanged(StatID statID)
    {
        var stat = statManager.GetStat(statID);
        slider.value = stat.Value / stat.MaxValue;
    }
}
