using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] protected PlayerDataSo playerData;     //Se asigna en el inspector

    [Header("Scripts")]
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private GameManager gameManager;

    // --------------------- COMPONENTES ---------------------
    private float currentSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [HideInInspector] public Vector2 movement;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [HideInInspector] public bool isDashing = false;
    [HideInInspector] public bool canDash = true;

    [Header("Damage")]
    private Color originalColor;

    [Header("Shoot Settings")]
    public Transform firePoint;     // punto desde donde se dispara
    public GameObject bulletPrefab; // asigna el prefab de la bala
    public float fireRate = 0.2f;   // intervalo entre disparos

    // --------------------- MÁQUINA DE ESTADOS ---------------------

    private List<State> states = new List<State>();
    [SerializeField] private State currentState;
    [SerializeField] private State previousState;

    // --------------------- MÉTODOS UNITY ---------------------

    private void Awake()
    {
        //componentes del player
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        //Configurar velocidad y la vida maxima del HeealthSystem segun el SO
        currentSpeed = playerData.baseSpeed;
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.Initialize(playerData.baseLife);
        healthSystem.onTakeDamage += OnTakeDamage;
        healthSystem.onDie += OnDie;
    }

    private void Start()
    {
        states.Add(new StateIdle(this, playerData));
        states.Add(new StateWalk(this, playerData));
        states.Add(new StateDash(this, playerData));
        states.Add(new StateShoot(this, playerData));
        states.Add(new StateHurt(this));
        states.Add(new StateDie(this));

        SwapStateTo(PlayerStates.Idle);
    }

    private void Update()
    {
        if (currentState != null)
            currentState.Update();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
            // 3. Aplicar movimiento con Rigidbody2D
            rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }


    // --------------------- GESTIÓN DE ESTADOS ---------------------
    public void SwapStateTo(PlayerStates nextState)
    {
        foreach (State state in states)
        {
            if (state.state == nextState)
            {
                currentState?.OnExit();
                previousState = currentState;
                currentState = state;
                currentState.OnEnter();
                break;
            }
        }
    }

    public void ChangeAnimatorState(int state)
    {
        animator.SetInteger("State", state);
    }

    // --------------------- FUNCIONES DE MECANICAS ---------------------

    public void Movement()
    {
        // 1. Capturar input
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(playerData.keyCodeLeft)) moveX = -1f;
        if (Input.GetKey(playerData.keyCodeRight)) moveX = 1f;
        if (Input.GetKey(playerData.keyCodeUp)) moveY = 1f;
        if (Input.GetKey(playerData.keyCodeDown)) moveY = -1f;

        // 2. Normalizar vector
        movement = new Vector2(moveX, moveY).normalized;
    }

    public IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        isDashing = true;

        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            rb.MovePosition(rb.position + direction * dashSpeed * Time.fixedDeltaTime);
            yield return null;
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void OnTakeDamage()
    {
        SwapStateTo(PlayerStates.Pain);
    }

    public IEnumerator FlashDamage(Color newColor)
    {
        spriteRenderer.color = newColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    private void OnDie()
    {
        SwapStateTo(PlayerStates.Die);
    }
    
    public void OnDeathAnimationEnd()
    {
        animator.SetTrigger("Die");
        StartCoroutine(DieCoroutine());
        // Este método será llamado automáticamente al final de la animación
    }

    public IEnumerator DieCoroutine()
    {
        gameManager.GameOver();
        Debug.Log("PLAYER MURIÓ (animación finalizada)");
        yield return new WaitForSeconds(1f);
    }
}

