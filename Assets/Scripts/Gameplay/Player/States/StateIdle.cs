using UnityEngine;

public class StateIdle : State
{
    public StateIdle(PlayerController playerController, PlayerDataSo playerData)
    {
        this.playerController = playerController;
        this.playerData = playerData;
        state = PlayerStates.Idle;
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
            playerController.Movement();
        }

        // Conexiones de Salida
        if (playerController.movement != Vector2.zero)
            playerController.SwapStateTo(PlayerStates.Walk);
        else if (Input.GetMouseButtonDown(playerData.fireMouseButton))
            playerController.SwapStateTo(PlayerStates.Shoot);
        else if (Input.GetKeyDown(playerData.keyCodeDash))
            playerController.SwapStateTo(PlayerStates.Dash); 

    }
}
