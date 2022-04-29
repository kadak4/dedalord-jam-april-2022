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
    private float targetValue, animationSpeed = 0.1f;

    void Start()
    {
        statManager = Locator.GetService<IStatsManager>();
        stat = statManager.GetStat(id);
        stat.OnStatValueChanged += OnValueChanged;
        OnValueChanged(id);
    }

    private void Update()
    {
        if (targetValue != slider.value)
        {
            var difference = targetValue - slider.value;
            if (Mathf.Abs(difference) < animationSpeed * Time.deltaTime)
            {
                slider.value = targetValue;
            }
            else
            {
                slider.value = Mathf.Lerp(slider.value, targetValue, animationSpeed);
            }
        }
    }

    void OnValueChanged(StatID statID)
    {
        var stat = statManager.GetStat(statID);
        targetValue = stat.Value / stat.MaxValue;
    }
}
