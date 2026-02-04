using UnityEngine;
using System.Collections;
//using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerHealth playerHealth;

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private bool isDashing = false;
    private bool canDash = true;

    [Header("Damage")]
    public int damage = 20;
    private float damageCooldown = 1f;
    private float lastDamageTime;
    private Color originalColor;

    private void Awake()
    {
        playerHealth.onTakeDamage += OnTakeDamage;
        playerHealth.onDie += OnDie;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (!isDashing)
        {
            // 1. Capturar input
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            // 2. Normalizar vector
            movement = new Vector2(moveX, moveY).normalized;
        }


        // 2. Input de dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            if (movement != Vector2.zero) // dash solo si hay dirección
                StartCoroutine(Dash(movement));
        }

    }

    private void FixedUpdate()
    {
        if (!isDashing)
            // 3. Aplicar movimiento con Rigidbody2D
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator Dash(Vector2 direction)
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && Time.time > lastDamageTime + damageCooldown)
        {
            playerHealth.DoDamage(damage);
            lastDamageTime = Time.time;
        }
    }

    private void OnTakeDamage()
    {
        StartCoroutine(CambiarColorTemporal(Color.red));
    }

    private IEnumerator CambiarColorTemporal(Color nuevoColor)
    {
        spriteRenderer.color = nuevoColor; 
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    private void OnDie()
    {
        Destroy(gameObject);
        //spriteRenderer.color = Random.ColorHSV();
    }

}

