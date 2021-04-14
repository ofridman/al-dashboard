using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepperText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI label;

    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    string formatString = "00";

    private void Start()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(int number)
    {
        label.text = number.ToString(formatString);
    }

    public void SetInputText(int number)
    {
        inputField.text = number.ToString(formatString);
    }

    public void SetAMPMText(int number)
    {
        label.text = number == 0 ? "AM" : "PM";
    }
}
