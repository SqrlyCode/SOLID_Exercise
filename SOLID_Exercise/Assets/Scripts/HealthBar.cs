using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float _desiredValue = 1;

    [SerializeField]
    private Image _img;
    [Range(0,0.3f)]
    [SerializeField]
    private float _changeDrag;

    private Color _desiredColor;
    
    private void FixedUpdate()
    {
        _img.fillAmount = Mathf.Lerp(_img.fillAmount, _desiredValue, Time.fixedDeltaTime / _changeDrag);
        _img.color = Color.Lerp(_img.color, _desiredColor, Time.fixedDeltaTime / _changeDrag);;
        UpdateDesiredColor();
    }

    public void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
    
    private void UpdateDesiredColor()
    {
        _desiredColor = Color.white;
        if (_desiredValue >= 0.5f)
        {
            _desiredColor = Color.Lerp(Color.yellow, Color.white, (_desiredValue-0.5f) * 2);
        }
        else
        {
            _desiredColor = Color.Lerp(Color.red, Color.yellow, _desiredValue * 2);
        }
    }
}
