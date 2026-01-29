using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform firePoint;     // punto desde donde se dispara
    [SerializeField] GameObject bulletPrefab; // asigna el prefab de la bala
    [SerializeField] float fireRate = 0.2f;   // intervalo entre disparos

    private float nextFireTime = 0f;

    void Update()
    {
        // Disparo continuo mientras se mantiene presionado el botón
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //bulletPrefab.GetComponent<Rigidbody2D>().AddForce (firePoint.up, ForceMode2D.Impulse);
    }
}
