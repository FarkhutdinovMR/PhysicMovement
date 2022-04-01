using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _healthBar;

    private void OnEnable()
    {
        _health.Dies += OnDies;
    }

    private void OnDisable()
    {
        _health.Dies -= OnDies;
    }

    private void OnDies()
    {
        _healthBar.SetActive(false);
    }
}