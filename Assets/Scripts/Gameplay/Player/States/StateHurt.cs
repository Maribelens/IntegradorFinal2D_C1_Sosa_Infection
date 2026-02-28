using UnityEngine;

public class StateHurt : State
{
    private float hurtDuration = 0.5f;  //tiempo que dura el estado de dolor
    private float startTime;

    public StateHurt(PlayerController playerController)
    {
        this.playerController = playerController;
        state = PlayerStates.Pain;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        playerController.ChangeAnimatorState((int)state);

        //Guarda el tiempo de inicio
        startTime = Time.time;

        //Efecto visual de danio
        playerController.StartCoroutine(playerController.HurtCoroutine(Color.red));
    }

    public override void Update()
    {
        //Durante el estado de dolor, bloqueamos movimiento y disparo
        if(Time.time >= startTime + hurtDuration)
        {
            //Si la vida llego a 0, pasar a Die
            if (playerController.movement == Vector2.zero)
                playerController.SwapStateTo(PlayerStates.Idle);
            else
                playerController.SwapStateTo(PlayerStates.Walk);
        }
    }
}
