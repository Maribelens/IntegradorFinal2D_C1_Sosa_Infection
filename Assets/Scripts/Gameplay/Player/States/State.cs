using UnityEngine;

public abstract class State
{
    public PlayerStates state { get; protected set; } = PlayerStates.None;
    protected PlayerController playerController;
    protected PlayerDataSo playerData;

    public virtual void OnEnter()
    {
        Debug.Log($"OnEnter de {state}");
    }

    public virtual void Update()
    {
        Debug.Log($"Update de {state}");
    }

    public virtual void OnExit()
    {
        Debug.Log($"OnExit de {state}");
    }
}
