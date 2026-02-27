using UnityEngine;

public class StateWalk : State
{
    public StateWalk(PlayerController playerController, PlayerDataSo playerData)
    {
        this.playerController = playerController;
        this.playerData = playerData;
        state = PlayerStates.Walk;
    }

    public override void OnEnter()
    {
        Debug.Log($"Entro de {state}");
        playerController.ChangeAnimatorState((int)state);
    }

    public override void Update()
    {
        if (!playerController.isDashing)
        {
            playerController.Movement();    //mantiene la actualizacion del Vector
        }

        // Si no hay input, volver a Idle
        if (playerController.movement == Vector2.zero)
            playerController.SwapStateTo(PlayerStates.Idle);

        // Si presiona disparo
        else if (Input.GetMouseButtonDown(playerData.fireMouseButton))
            playerController.SwapStateTo(PlayerStates.Shoot);

        // Si presiona dash
        else if (Input.GetKeyDown(playerData.keyCodeDash))
            playerController.SwapStateTo(PlayerStates.Dash);
    }
}