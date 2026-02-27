using UnityEngine;

public class StateShoot : State
{
    private float nextFireTime = 0f;

    public StateShoot(PlayerController playerController, PlayerDataSo playerData)
    {
        this.playerController = playerController;
        this.playerData = playerData;
        state = PlayerStates.Shoot;
    }

    public override void OnEnter()
    {
        Debug.Log($"Entro en {state}");
        playerController.ChangeAnimatorState((int)state);
        nextFireTime = Time.time;

        playerController.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public override void Update()
    {
        // Disparo continuo mientras se mantiene presionado el botón
        if (Input.GetMouseButton(playerData.fireMouseButton) && Time.time >= nextFireTime)
        {
            playerController.Shoot();
            nextFireTime = Time.time + playerController.fireRate;
        }

        if (!Input.GetMouseButton(playerData.fireMouseButton))
        {
            if (playerController.movement == Vector2.zero)
                playerController.SwapStateTo(PlayerStates.Idle);
            else
                playerController.SwapStateTo(PlayerStates.Walk);
        }

    }

    public override void OnExit()
    {
        //Debug.Log($"Salgo de {state}");
    }
}
