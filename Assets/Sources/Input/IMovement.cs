using System;
using UnityEngine;

public interface IMovement
{
    public event Action<float> SpeedChanged;

    public void Move(Vector2 direction);

    public void Run();

    public void StopRun();

    public void Jump();
}