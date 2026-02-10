using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    // Eventos de estado de vida e invulnerabilidad
    public event Action<int, int> onLifeUpdated;    // (vida actual, vida máxima)
    public event Action onTakeDamage;
    public event Action onDie;

    public int maxLife = 100;
    private int currentLife;
    public bool isInvulnerable = false;

    private void Awake()
    {
        currentLife = maxLife;
    }

    private void Start()
    {
        onLifeUpdated?.Invoke(currentLife, maxLife);
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0)
        {
            Debug.Log("Se cura en la funcion de daño");
            return;
        }

        if (isInvulnerable) return;

        currentLife -= amount;

        if (currentLife <= 0)
        {
            onDie?.Invoke();
        }
        else
        {
            onTakeDamage?.Invoke();
            onLifeUpdated?.Invoke(currentLife, maxLife);
        }

        Debug.Log("DoDamage", gameObject);
    }

    public void Heal(int plus)
    {
        if (plus < 0)
        {
            Debug.Log("Se daña en la funcion de cura");
            return;
        }

        currentLife += plus;

        if (currentLife > maxLife)
            currentLife = maxLife;

        Debug.Log("Heal");
        onLifeUpdated?.Invoke(currentLife, maxLife);
        Debug.Log("Curación aplicada");
    }
}