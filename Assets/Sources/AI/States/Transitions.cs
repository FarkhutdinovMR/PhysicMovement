using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Transitions
{
    [SerializeField] private Transition[] _transition;

    public void Enable()
    {
        foreach (Transition transition in _transition)
            transition.enabled = true;
    }
}