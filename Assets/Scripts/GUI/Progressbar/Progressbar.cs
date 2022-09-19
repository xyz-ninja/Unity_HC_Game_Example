using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour {
    
    [SerializeField] private Image _fiilImage;
    [SerializeField] protected Transform _rootT;
    
    private float _maxValue;
    private float _currentValue;
    
    public void SetValues(float maxValue, float currentValue) {
        _maxValue = maxValue;
        _currentValue = currentValue;
        
        UpdateView();
    }

    public void SetCurrentValue(float currentValue) {

        _currentValue = currentValue;
        
        UpdateView();
    }

    protected void UpdateView() {
        _fiilImage.fillAmount = _currentValue / _maxValue;
    }
}
