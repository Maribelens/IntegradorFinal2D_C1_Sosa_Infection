using System.Collections;
using UnityEngine;

public class CriticalObjective : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private HealthSystem healthSystem;

    //public int generatorHealth = 100;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.onTakeDamage += OnTakeDamage;
        healthSystem.onDie += OnDie;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnTakeDamage()
    {
        StartCoroutine(ChangeTemporaryColor(Color.yellow));
    }

    IEnumerator ChangeTemporaryColor(Color newColor)
    {
        spriteRenderer.color = newColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    private void OnDie()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
    }

}
