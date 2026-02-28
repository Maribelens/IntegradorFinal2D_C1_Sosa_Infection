using UnityEngine;
using System.Collections;
using System;

public abstract class EnemyBase : MonoBehaviour
{
    public event Action<EnemyBase> OnEnemyDeath;

    [Header("Enemy Data")]
    [SerializeField] protected EnemyDataSo enemyData;     //Se asigna en el inspector

    [Header("Scripts")]
    [SerializeField] protected HealthSystem healthSystem;
    [SerializeField] protected GameManager gameManager;

    protected int currentDamage;
    [HideInInspector] public float baseSpeed;
    [SerializeField] private float maxSpeed = 5f;
    //[SerializeField] private float speedMultiplier = 0.05f;
    protected float currentSpeed;
    [SerializeField] private GameObject vfxHurtrefab;
    [SerializeField] private GameObject vfxDiePrefab;
 

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
        currentDamage = enemyData.baseDamage;
        currentSpeed = enemyData.baseSpeed;

        //gameManager = GetComponent<GameManager>();
        //healthSystem = GetComponent<HealthSystem>();

        //Configurar la vida maxima del HeealthSystem segun el SO
        healthSystem.Initialize(enemyData.baseLife);
        //Debug.Log("EnemyBase Awake ejecutado", gameObject);

        //healthSystem.maxLife = enemyData.baseLife;

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
        //healthSystem = GetComponent<HealthSystem>();
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
        GameObject bloodSplash = Instantiate(vfxHurtrefab, transform.position, Quaternion.identity);
        Destroy(bloodSplash, 0.5f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    protected virtual void OnDie()
    {
        OnEnemyDeath?.Invoke(this);
        GameObject bloodSplash = Instantiate(vfxDiePrefab, transform.position, Quaternion.identity);
        Destroy(bloodSplash, 0.5f);
        Destroy(gameObject);
    }
}
