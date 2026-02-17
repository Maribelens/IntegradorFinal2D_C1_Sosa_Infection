using UnityEngine;

[CreateAssetMenu(fileName = "ObjetiveData", menuName = "Game/Objetive Data")]

public class ObjectiveDataSo : ScriptableObject
{
    [Header("Critical Objetive Settings")]
    public string objetiveName = "Generator";
    public int baseLife = 200;
    //public float baseSpeed;
    //public int baseDamage;
}
