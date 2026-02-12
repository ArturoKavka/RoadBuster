using UnityEngine;
using TMPro;

public class WaveUIController : MonoBehaviour
{
    [Header("Referencias de UI")]
    public TextMeshProUGUI waveNumberText;

    public GameObject skipButton;

    private const int MAXIMO_OLEADAS = 30;
    private const float SEGUNDOS_PARA_SALTAR = 60f;

    void Start()
    {
        if (skipButton != null)
        {
            skipButton.SetActive(false);
        }
    }

    public void UpdateUI(int currentWave, float timeRemaining)
    {
        if (currentWave > MAXIMO_OLEADAS)
        {
            waveNumberText.text = "Oleada: <color=red>∞ INFINITA</color>";
        }
        else
        {
            waveNumberText.text = $"Oleada {currentWave} / {MAXIMO_OLEADAS}";
        }

        bool showSkipButton = currentWave <= MAXIMO_OLEADAS
                            && timeRemaining <= SEGUNDOS_PARA_SALTAR
                            && timeRemaining > 0;

        if (skipButton != null)
        {
            skipButton.SetActive(showSkipButton);
        }
    }
}