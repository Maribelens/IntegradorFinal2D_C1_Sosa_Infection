using UnityEngine;
using UnityEngine.UI;

public class UiInfectionBar : MonoBehaviour
{
    [SerializeField] private Image infectionFill;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Gradient infectionGradient;
    private float targetValue;

    private void OnEnable()
    {
        gameManager.OnInfectionChanged += UpdateUI;
    }

    private void Start()
    {
        UpdateUI(gameManager.Infection01);
    }

    private void Update()
    {
        //modificacion con interpolación suave
        infectionFill.fillAmount = Mathf.Lerp(infectionFill.fillAmount, targetValue, Time.deltaTime * 5f);
    }

    private void OnDisable()
    {
        gameManager.OnInfectionChanged -= UpdateUI;
    }

    private void UpdateUI(float value)
    {
        //modificacion constante del color
        targetValue = value;
        //infectionFill.fillAmount = value;
        infectionFill.color = infectionGradient.Evaluate(value);
    }



}
