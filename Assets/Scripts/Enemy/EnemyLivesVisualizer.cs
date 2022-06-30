using UnityEngine;
using UnityEngine.UI;

public class EnemyLivesVisualizer : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private Slider slider;

    EnemyLivesManager enemyLivesManager;
    void Start()
    {
        enemyLivesManager = gameObject.GetComponent<EnemyLivesManager>();
        Debug.Assert(enemyLivesManager != null, "Enemy must have EnemyLivesManager component");
        slider.minValue = 0;
        slider.maxValue = enemyLivesManager.InitialHealth();
        slider.fillRect.GetComponent<Image>().color = color;
    }

    void Update()
    {
        slider.value = System.Math.Clamp(enemyLivesManager.CurrentHealth(), slider.minValue, slider.maxValue);
    }
}
