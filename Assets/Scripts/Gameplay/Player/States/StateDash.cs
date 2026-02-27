using UnityEngine;

public class StateDash : State
{
    public StateDash(PlayerController playerController, PlayerDataSo playerData)
    {
        this.playerController = playerController;
        this.playerData = playerData;
        state = PlayerStates.Dash;
    }

    public override void OnEnter()
    {
        Debug.Log($"Entro de {state}");
        playerController.ChangeAnimatorState((int)state);
        playerController.StartCoroutine(playerController.Dash(playerController.movement));
    }

    public override void Update()
    {
        // 2. Input de dash
        if (Input.GetKeyDown(playerData.keyCodeDash) && playerController.canDash)
        {
            if (playerController.movement != Vector2.zero) // dash solo si hay dirección
                playerController.StartCoroutine(playerController.Dash(playerController.movement));
        }

        if (!playerController.isDashing)
        {
            if (playerController.movement == Vector2.zero)
                playerController.SwapStateTo(PlayerStates.Idle);
            else
                playerController.SwapStateTo(PlayerStates.Walk);
        }
    }

    public override void OnExit()
    {
        // Disparo particulas de polvito
    }
}
