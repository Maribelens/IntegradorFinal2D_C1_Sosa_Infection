using UnityEngine;
using UnityEngine.UI;

public class UiLifeBar : MonoBehaviour
{
    [Header("LifeBar Panel")]
    [SerializeField] private HealthSystem lifeTarget;
    [SerializeField] private Image lifeBar;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 1f, 0);
    [SerializeField] private bool isWorldSpace = true;

    private void Awake()
    {
        // Suscripci�n a eventos del sistema de salud
        lifeTarget.onLifeUpdated += UpdateLifeBar;
        lifeTarget.onDie += EmptyLifeBar;
    }

    private void LateUpdate()
    {
        if (!isWorldSpace) return;

        if (target == null) return;
        transform.rotation = Quaternion.identity;
        transform.position = target.position + offset;
    }

    private void OnDestroy()
    {
        // Evita referencias colgantes al destruir el objeto
        lifeTarget.onLifeUpdated -= UpdateLifeBar;
        lifeTarget.onDie -= EmptyLifeBar;

        // Si no se asignó en el inspector, buscar el target automáticamente
        if (target == null && lifeTarget != null) 
            target = lifeTarget.transform;
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
