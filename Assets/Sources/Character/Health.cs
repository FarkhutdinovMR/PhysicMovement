using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IValue
{
    [SerializeField] private float _maxValue;

    public event Action<float, float> Changed;

    public event Action DamageRecieved;

    public event Action Dies;

    public bool IsAlive => _value > 0;

    private float _value;

    private void Start()
    {
        _value = _maxValue;
    }

    public void TakeDamage(float value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        if (IsAlive == false)
            throw new InvalidOperationException();

        _value -= value;

        if (_value <= 0)
        {
            _value = 0;
            Dies?.Invoke();
        }
        else
        {
            DamageRecieved?.Invoke();
        }

        Changed?.Invoke(_value, _maxValue);
    }
}