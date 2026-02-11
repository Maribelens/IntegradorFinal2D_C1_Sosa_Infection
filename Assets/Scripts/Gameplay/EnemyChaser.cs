using UnityEngine;

public class EnemyChaser : EnemyBase
{
    private Rigidbody2D rb;

    [Header("Damage")]
    private float damageCooldown = 1f;
    private float lastDamageTime;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        if (damageable != null && Time.time > lastDamageTime + damageCooldown)
        {
            damageable.TakeDamage(damage);
            lastDamageTime = Time.time;
        }
    }
}
