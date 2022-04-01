using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour, IValue
{
    [SerializeField] private float _maxValue;
    [SerializeField] private float _delayBeforeRecovery;
    [SerializeField] private float _recoverySpeed;

    private float _value;
    private Coroutine _recoveryCoroutine;

    public event Action<float, float> Changed;

    public float Value => _value;

    public bool HasPower => _value > 0;

    public bool isEmty => _value == 0;

    private void OnValidate()
    {
        if (_maxValue > 0)
            return;

        Debug.LogWarning(nameof(_maxValue) + " must be positive number");
        _maxValue = 0;
    }

    private void Start()
    {
        _value = _maxValue;
    }

    public void Spend(float value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _value -= value;

        if (_value < 0)
            _value = 0;

        if (_recoveryCoroutine != null)
            StopCoroutine(_recoveryCoroutine);

        _recoveryCoroutine = StartCoroutine(Recovery());

        Changed?.Invoke(_value, _maxValue);
    }

    private IEnumerator Recovery()
    {
        yield return new WaitForSeconds(_delayBeforeRecovery);

        while(Value < _maxValue)
        {
            _value = Mathf.MoveTowards(_value, _maxValue, _recoverySpeed * Time.deltaTime);
            Changed?.Invoke(_value, _maxValue);

            yield return null;
        }

        _recoveryCoroutine = null;
    }
}