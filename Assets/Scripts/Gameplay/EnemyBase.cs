using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour, IDamageable
{
    public int maxLife = 50;
    private int currentLife;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    protected virtual void Awake()
    {
        currentLife = maxLife;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int amount)
    {
        currentLife -= amount;
        if (currentLife <= 0)
            Die();
        else
            StartCoroutine(FlashDamage());
    }

    private IEnumerator FlashDamage()
    {
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
