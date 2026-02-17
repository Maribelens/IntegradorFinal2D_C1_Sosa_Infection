using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyDataSo : ScriptableObject
{
    [Header("Enemy Settings")]
    public string enemyName = "Chaser";
    public int baseLife = 50;
    public float baseSpeed = 2f;
    public int baseDamage = 10;
}

