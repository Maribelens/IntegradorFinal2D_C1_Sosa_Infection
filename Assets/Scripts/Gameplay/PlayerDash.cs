using UnityEngine;
//using System.Collections;
public class PlayerDash : MonoBehaviour
{
    ////public float dashSpeed = 20f;
    ////public float dashDuration = 0.2f;
    ////public float dashCooldown = 1f;

    ////private Rigidbody2D rb;
    ////private bool isDashing = false;
    ////private bool canDash = true;

    ////private void Start()
    ////{
    ////    rb = GetComponent<Rigidbody2D>();
    ////}

    ////private void Update()
    ////{
    ////    if (Input.GetKeyDown(KeyCode.Space) && canDash)
    ////    {
    ////        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    ////        if (dashDirection != Vector2.zero)
    ////            StartCoroutine(Dash(dashDirection.normalized));
    ////    }
    ////}

    ////private IEnumerator Dash(Vector2 direction)
    ////{
    ////    canDash = false;
    ////    isDashing = true;

    ////    float startTime = Time.time;
    ////    while (Time.time < startTime + dashDuration)
    ////    {
    ////        rb.velocity = direction * dashSpeed;
    ////        yield return null;
    ////    }

    ////    rb.velocity = Vector2.zero;
    ////    isDashing = false;

    ////    yield return new WaitForSeconds(dashCooldown);
    ////    canDash = true;
    ////}
}
