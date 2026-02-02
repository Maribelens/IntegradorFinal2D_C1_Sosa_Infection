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
        // 1. Capturar input
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // 2. Normalizar vector
        movement = new Vector2(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        // 3. Aplicar movimiento con Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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

