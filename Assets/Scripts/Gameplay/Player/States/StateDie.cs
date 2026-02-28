using JetBrains.Annotations;
using UnityEngine;

public class StateDie : State
{
    //private float deathAnimation = 1.5f;
    //private float startTime;

    public StateDie(PlayerController playerController)
    {
        this.playerController = playerController;
        state = PlayerStates.Die;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        playerController.ChangeAnimatorState((int)state);

        //Guarda el tiempo de inicio
        //startTime = Time.time;

        //Desactivar movimiento
        playerController.movement = Vector2.zero;

        //Efecto visual de danio
        playerController.StartCoroutine(playerController.DieCoroutine());
    }

    //public override void Update()
    //{
    //    if (Time.time >= startTime + deathAnimation)
    //    {
    //        playerController.OnDeathAnimationEnd();
    //    }
    //}
}
