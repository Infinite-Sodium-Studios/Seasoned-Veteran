using UnityEngine;
using TMPro;


public class PlayerSpeedTextUpdate : MonoBehaviour
{
    [SerializeField] private QuakeFirstPersonController controller;
    [SerializeField] private TextMeshProUGUI textElement;

    void Update()
    {
        var speed = controller.GetSpeed();
        speed = Mathf.Round(speed * 10f);
        string textValue = speed.ToString();
        while (textValue.Length < 3) {
            textValue = " " + textValue;
        }
        textElement.text = textValue;
    }
}
