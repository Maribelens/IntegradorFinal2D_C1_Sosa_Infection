using UnityEngine;
using UnityEngine.UI;

public class UiLifeBar : MonoBehaviour
{
    [Header("LifeBar Panel")]
    [SerializeField] private HealthSystem target;
    [SerializeField] private Image lifeBar;

    private void Awake()
    {
        // Suscripci�n a eventos del sistema de salud
        target.onLifeUpdated += UpdateLifeBar;
        target.onDie += EmptyLifeBar;
    }

    private void OnDestroy()
    {
        // Evita referencias colgantes al destruir el objeto
        target.onLifeUpdated -= UpdateLifeBar;
        target.onDie -= EmptyLifeBar;
    }

    public void UpdateLifeBar(int current, int max)
    {
        // Actualiza la barra de vida seg�n el porcentaje restante
        float lerp = current / (float)max;
        lifeBar.fillAmount = lerp;
        //Debug.Log($"[UI] fillAmount set to {lifeBar.fillAmount}");
        //Debug.Log($"[UI] Life updated: {current}/{max}");
    }

    private void EmptyLifeBar()
    {
        // Vac�a la barra al morir el jugador
        lifeBar.fillAmount = 0;
    }
}
