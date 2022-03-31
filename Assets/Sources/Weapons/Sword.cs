using System;
using System.Collections;
using UnityEngine;

public class Sword : DefaultAttack, ISimpleAttack, IRunningAttack
{
    public void Attack(Action onEnd)
    {
        Attack(AnimatorPaladinController.States.Attack, onEnd);
    }

    public void AttackInRunning(Action onEnd)
    {
        Attack(AnimatorPaladinController.States.RunningAttack, onEnd);
    }    
}