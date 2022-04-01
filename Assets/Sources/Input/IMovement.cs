using System;
using UnityEngine;

public interface IMovement
{
    public event Action<float> SpeedChanged;

    public void MoveForward(float delta);

    public void Run();

    public void StopRun();

    public void Jump();
}