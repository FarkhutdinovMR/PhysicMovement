using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : DefaultAttack, ISimpleAttack
{
    public void Attack(Action onEnd)
    {
        Attack(AnimatorPaladinController.States.Kick, onEnd);
    }
}