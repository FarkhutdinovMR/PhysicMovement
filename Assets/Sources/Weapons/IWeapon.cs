using System;

public interface IBlock
{
    public void Use();

    public void StopUse();
}

public interface ISimpleAttack
{
    public void Attack(Action onEnd);
}

public interface IRunningAttack
{
    public void AttackInRunning(Action onEnd);
}