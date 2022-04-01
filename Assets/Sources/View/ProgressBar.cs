using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IValue
{
    public event Action<float, float> Changed;
}

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _slider;
    [SerializeField] private MonoBehaviour _valueObject;

    private IValue _value => (IValue)_valueObject;

    //private void OnValidate()
    //{
    //    if (_valueObject is IValue)
    //        return;

    //    Debug.Log(_valueObject.name + " not implement " + nameof(IValue));
    //    _valueObject = null;
    //}

    private void OnEnable()
    {
        _value.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _value.Changed -= OnChanged;
    }

    private void OnChanged(float value, float _maxValue)
    {
        _slider.fillAmount = value / _maxValue;
    }
}