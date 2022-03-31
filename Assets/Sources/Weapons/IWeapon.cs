using System;

public interface IWeapon
{
    public void Use(Action onEnd);

    public void Use();

    public void StopUse();
}
