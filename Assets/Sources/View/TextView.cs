using UnityEngine;
using TMPro;

public class TextView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _name;
    [SerializeField] private string _format;

    public void UpdateText(float value)
    {
        _text.SetText($"{_name}: {value.ToString(_format)}");
    }

    public void UpdateText(Vector3 value)
    {
        _text.SetText($"{_name}: {value.ToString(_format)}");
    }

    public void UpdateText(string value)
    {
        _text.SetText($"{_name}: {value}");
    }
}