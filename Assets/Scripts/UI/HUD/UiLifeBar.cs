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
        target.onLifeUpdated += HealthSystem_onLifeUpdated;
        target.onDie += HealthSystem_onDie;
    }

    private void OnDestroy()
    {
        // Evita referencias colgantes al destruir el objeto
        target.onLifeUpdated -= HealthSystem_onLifeUpdated;
        target.onDie -= HealthSystem_onDie;
    }

    public void HealthSystem_onLifeUpdated(int current, int max)
    {
        // Actualiza la barra de vida seg�n el porcentaje restante
        float lerp = current / (float)max;
        lifeBar.fillAmount = lerp;
    }

    private void HealthSystem_onDie()
    {
        // Vac�a la barra al morir el jugador
        lifeBar.fillAmount = 0;
    }
}
