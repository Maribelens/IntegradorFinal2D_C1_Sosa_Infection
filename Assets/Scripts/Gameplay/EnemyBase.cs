using UnityEngine;
using System.Collections;
using System;

public abstract class EnemyBase : MonoBehaviour
{
    public event Action<EnemyBase> OnEnemyDeath;

    [Header("Scripts")]
    [SerializeField] protected HealthSystem healthSystem;
    [SerializeField] protected GameManager gameManager;

    [Header("Enemy Stats")]
    //public int life = 50;
    public int damage = 10;
    [HideInInspector] public float baseSpeed = 2f;
    [SerializeField] private float maxSpeed = 5f;
    //[SerializeField] private float speedMultiplier = 0.05f;
    private float currentSpeed;

    [Header ("References")]
    protected Transform player; //referencia al jugador

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
    }
    protected virtual void Awake()
    {
        gameManager = GetComponent<GameManager>();
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.onTakeDamage += OnTakeDamage;
        healthSystem.onDie += OnDie;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    protected virtual void OnEnable()
    {
        if (gameManager != null)
            gameManager.OnInfectionChanged += HandleInfectionChanged;
    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        HandleInfectionChanged(gameManager.Infection01);
    }

    protected virtual void OnDisable()
    {
        if(gameManager != null)
        gameManager.OnInfectionChanged -= HandleInfectionChanged;
    }

    private void HandleInfectionChanged(float infection01)
    {
        float evaluated = gameManager.InfectionEvaluated;
        currentSpeed = Mathf.Lerp(baseSpeed, maxSpeed, evaluated);
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
