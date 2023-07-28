using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    private Image _selfImage;

    [SerializeField] Color _normalColor;
    [SerializeField] Color _selectedColor;

    private void Awake()
    {
        _selfImage = GetComponent<Image>();
    }

    private void Start()
    {
        ResetColor();
    }

    public void ResetColor()
    {
        _selfImage.color = _normalColor;
    }

    public void ChangeColor()
    {
        if (_selfImage.color == _normalColor)
        {
            _selfImage.color = _selectedColor;    // Normal color to selected color
        }
        else _selfImage.color = _normalColor;     // Selected color to normal color
    }
}
