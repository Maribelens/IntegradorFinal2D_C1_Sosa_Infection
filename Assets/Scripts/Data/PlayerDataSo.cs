using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]

public class PlayerDataSo : ScriptableObject
{
    [Header("Player Settings")]
    public string playerName = "Doctor";
    public int baseLife = 100;
    public float baseSpeed = 5f;
    public int baseDamage = 10;

    [Header("Inputs")]
    public KeyCode keyCodeLeft = KeyCode.LeftArrow;
    public KeyCode keyCodeRight = KeyCode.RightArrow;
    public KeyCode keyCodeUp = KeyCode.UpArrow;
    public KeyCode keyCodeDown = KeyCode.DownArrow;
    public KeyCode keyCodeDash = KeyCode.Space;
    public int fireMouseButton = 0; // 0 = click izquierdo

    //[Header("Bullet")]
    //public GameObject bulletPrefab;
}
