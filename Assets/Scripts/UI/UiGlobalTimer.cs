using TMPro;
using UnityEngine;

public class UiGlobalTimer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public float tiempoObjetivo = 60f;
    private float tiempoRestante;
    public TextMeshProUGUI textoTiempo;

    void Start()
    {
        tiempoRestante = tiempoObjetivo;
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            textoTiempo.text = Mathf.Ceil(tiempoRestante).ToString();
        }
        else
        {
            gameManager.Victory();    
        }
    }

}
