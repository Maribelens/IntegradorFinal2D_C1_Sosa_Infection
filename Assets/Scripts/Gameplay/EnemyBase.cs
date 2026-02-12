using UnityEngine;
using System.Collections;
using System;

public abstract class EnemyBase : MonoBehaviour
{
    public event Action<EnemyBase> OnEnemyDeath;

    [Header("Scripts")]
    [SerializeField] private HealthSystem healthSystem;

    [Header("Enemy Stats")]
    public int life = 50;
    public int damage = 10;
    public float speed = 2f;

    [Header ("References")]
    protected Transform player; //referencia al jugador

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.onTakeDamage += OnTakeDamage;
        healthSystem.onDie += OnDie;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void OnTakeDamage()
    {
        StartCoroutine(FlashDamage());
    }

    private IEnumerator FlashDamage()
    {
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    protected virtual void OnDie()
    {
        OnEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
