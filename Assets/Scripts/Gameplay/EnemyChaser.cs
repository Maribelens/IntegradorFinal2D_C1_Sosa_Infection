using System.Collections;
using UnityEngine;

public class EnemyChaser : EnemyBase
{
    private Rigidbody2D rb;
    private Transform objetive;
    private Transform currentTarget;

    [Header("Damage")]
    [SerializeField] private float attackInterval = 1f; //Tiempo entre golpes
    [SerializeField] private float damageCooldown = 1f;
    private float lastDamageTime;
    private Coroutine attackCoroutine;
    private float nextCheckTime;

    [Header("AI Settings")]
    [SerializeField] private float checkInterval = 0.5f;
    [SerializeField] private float attackRangeObjective = 2f;
    [SerializeField] private float attackRangePlayer = 1.5f;
    [SerializeField] private bool preferObjective = true;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        objetive = GameObject.FindGameObjectWithTag("Objetive")?.transform;
        currentTarget = player;

    }

    protected virtual void Update()
    {
        if (player == null && objetive == null) return;

        if(Time.time >= nextCheckTime)
        {
            SelectTarget();
            nextCheckTime = Time.time + checkInterval;
        }
    }

    private void FixedUpdate()
    {
        if (currentTarget == null) return;
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * baseSpeed * Time.fixedDeltaTime);
    }

    private void SelectTarget()
    {
        //Si el player fue destruido, ignoralo
        bool playerAlive = player != null;
        bool objetiveAlive = objetive != null;

        if(!playerAlive && !objetiveAlive)
        {
            currentTarget = null;
            return;
        }

        if (!objetiveAlive)
        {
            currentTarget = player;
            return;
        }

        if (!playerAlive)
        {
            currentTarget = objetive;
            return;
        }

        float distanceObjetive = Vector2.Distance(transform.position, objetive.position);
        float distancePlayer = Vector2.Distance(transform.position, player.position);

        if (preferObjective && distanceObjetive <= attackRangeObjective)
        {
            currentTarget = objetive;
            return;
        }

        if (distancePlayer <= attackRangePlayer)
        {
            currentTarget = player;
            return;
        }

        //  fallback: siempre perseguir al jugador
        currentTarget = player;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Solo aplicar daño si colisiona con el target actual
        Debug.Log("Colisión con: " + collision.collider.name);

        if (collision.transform != currentTarget) return;

        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        if (damageable != null && attackCoroutine == null)
        {
            //Inicia corrutina de ataque continuo
            attackCoroutine = StartCoroutine(AttackCoroutine(damageable));
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform != currentTarget) return;

        if (attackCoroutine != null)
        {
            //Detiene la corrutina al salir del contacto
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    private IEnumerator AttackCoroutine(IDamageable damageable)
    {
        while (damageable != null && Time.time > lastDamageTime + damageCooldown)
        {
            damageable.TakeDamage(10);
            lastDamageTime = Time.time;
            yield return new WaitForSeconds(attackInterval);
        }
    }
}
