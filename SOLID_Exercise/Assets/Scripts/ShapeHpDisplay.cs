using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShapeHpDisplay : MonoBehaviour
{
    public float _desiredValue = 1;
    
    [Range(0,0.3f)]
    [SerializeField] private float _changeDrag;
    
    private Color _startColor;
    private SpriteRenderer _spriteRenderer;

    private Color _desiredColor;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
    }

    private void Update()
    {
        //Update Scale
        float newScale = Mathf.Lerp(_spriteRenderer.transform.localScale.x, 1-_desiredValue, Time.fixedDeltaTime / _changeDrag);
        _spriteRenderer.transform.localScale = Vector3.one * newScale;
        _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _desiredColor, Time.fixedDeltaTime / _changeDrag);;
        
        //Update Color
        Color black;
        ColorUtility.TryParseHtmlString("#16202A", out black);
        _desiredColor = Color.Lerp(black, _startColor, _desiredValue);
    }

    public void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}
