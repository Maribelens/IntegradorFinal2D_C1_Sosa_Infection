using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private PlayerDataSo playerData;
    private int playerCurrentDamage;
    public enum DamageOwner
    {
        Player,
        Enemy
    }

    [SerializeField] private DamageOwner owner;
    //[SerializeField] private int damage = 10;

    public float speed = 10f;
    public float lifeTime = 2f; // tiempo antes de destruir la bala
    private Rigidbody2D rb;

    private void Awake()
    {
        playerCurrentDamage = playerData.baseDamage;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime); // destruye la bala después de cierto tiempo
    }

    private void Update()
    {
        rb.velocity = transform.up * speed; // mueve la bala hacia adelante
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Aquí puedes manejar daño o efectos
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        if (damageable == null) return;
        if (owner == DamageOwner.Player && collision.collider.CompareTag("Enemy"))
        {
            damageable.TakeDamage(playerCurrentDamage);
        }
        Destroy(gameObject); // destruye la bala al chocar
    }
}