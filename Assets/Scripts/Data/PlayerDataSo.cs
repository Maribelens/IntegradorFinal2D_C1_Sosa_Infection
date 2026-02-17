using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]

public class PlayerDataSo : ScriptableObject
{
    [Header("Player Settings")]
    public string playerName = "Doctor";
    public int baseLife = 100;
    public float baseSpeed = 5f;
    public int baseDamage = 10;

    //[Header("Bullet")]
    //public GameObject bulletPrefab;
}
