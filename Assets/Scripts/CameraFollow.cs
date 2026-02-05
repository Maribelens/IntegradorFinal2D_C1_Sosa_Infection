using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target a seguir")]
    public Transform player;          // El objeto del jugador

    [Header("Configuracion")]
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset fijo
    public float smoothSpeed = 5f;    // Velocidad de suavizado

    void LateUpdate()
    {
        if (player == null) return;

        // Posicion deseada = posicion del jugador + offset
        Vector3 desiredPosition = player.position + offset;

        // Interpolacion suave (Lerp) entre la posicion actual y la deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Asignar la posicion suavizada a la cámara
        transform.position = smoothedPosition;
    }
}
