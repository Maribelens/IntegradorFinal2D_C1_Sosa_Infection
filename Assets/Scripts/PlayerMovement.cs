using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Capturar input
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // 2. Normalizar vector
        movement = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // 3. Aplicar movimiento con Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}


//using UnityEngine;

//public class Bullet : MonoBehaviour
//{
//    public float speed = 10f;
//    public float lifeTime = 2f; // tiempo antes de destruir la bala

//    private Rigidbody2D rb;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        rb.velocity = transform.up * speed; // mueve la bala hacia adelante
//        Destroy(gameObject, lifeTime); // destruye la bala después de cierto tiempo
//    }

//    void OnCollisionEnter2D(Collision2D collision)
//    {
//        // Aquí puedes manejar daño o efectos
//        Destroy(gameObject); // destruye la bala al chocar
//    }
//}

