using UnityEngine;

public abstract class Sim
{
    public abstract void EnterState(StateManager sim);

    public abstract void UpdateState(StateManager sim);

    public abstract void OnExit(StateManager sim);
}