using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUiControll : MonoBehaviour
{
    [SerializeField] private Slider _hitPointsSlider;
    [SerializeField] private Slider _strengthValueSlider;
    private void Update()
    {
        SetSlidersValue();
    }
    private void SetSlidersValue() 
    {
        _hitPointsSlider.value = PlayerStats.Instance.GetHPValue();
        _strengthValueSlider.value = PlayerStats.Instance.GetStrengthValue();
    }
}
