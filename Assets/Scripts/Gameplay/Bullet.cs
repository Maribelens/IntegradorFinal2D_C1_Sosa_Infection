using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f; // tiempo antes de destruir la bala

    private Rigidbody2D rb;

    void Start()
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
        if(collision.collider.CompareTag("Enemy"))
        Destroy(gameObject); // destruye la bala al chocar
    }
}