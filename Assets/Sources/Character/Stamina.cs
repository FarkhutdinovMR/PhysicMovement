using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour, IValue
{
    [SerializeField] private float _maxValue;
    [SerializeField] private float _recoverySpeed;

    private float _value;

    public event Action<float, float> Changed;

    public float Value => _value;

    public bool HasPower => _value > 0f;

    public bool isEmty => _value == 0;

    private void Start()
    {
        _value = _maxValue;
    }

    private void Update()
    {
        _value = Mathf.MoveTowards(_value, _maxValue, _recoverySpeed * Time.deltaTime);
        Changed?.Invoke(_value, _maxValue);
    }

    public void Spend(float value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _value -= value;

        if (_value < 0)
            _value = 0;

        Changed?.Invoke(_value, _maxValue);
    }
}