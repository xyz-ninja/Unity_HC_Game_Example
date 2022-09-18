using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class IntClamped
{
    [SerializeField] private int _maxValue = 3;
    [SerializeField] private int _currentValue = 3;

    [Header("Finish Value")]
    [SerializeField] private bool _isHasFinishValue = false;
    [SerializeField] private int _finishValue = 0;
    [SerializeField] private bool _isFinishOnReduce = true; 
    
    private bool _isOnFinishValue = false;

    public event Action ValueChanged;
    public int CurrentValue => _currentValue;
    public int MaxValue => _maxValue;

    private void Start() {
        
        CheckStatus();
    }

    public void Reset() {
        
        _currentValue = _maxValue;
        
        ValueChanged?.Invoke();
    }

    public void Add(int count) {
        
        _currentValue += count;

        if (_currentValue > _maxValue) {
            _currentValue = _maxValue;
        }
        
        ValueChanged?.Invoke();

        CheckStatus();
    }

    public void Reduce(int _count) {

        _currentValue -= _count;
	
        if (_currentValue < 0) {
            _currentValue = 0;
        }
        
        ValueChanged?.Invoke();

        CheckStatus();
    }

    void CheckStatus() {
        
        if (_currentValue <= _finishValue && _isFinishOnReduce ) {
            
            Finish();
            
        } else if (_currentValue >= _finishValue && _isFinishOnReduce == false) {
            
            Finish();
            
        } else {
            
            _isOnFinishValue = false;
        }
    }

    void Finish() {
        if (_isOnFinishValue == false) {
            _isOnFinishValue = true;
        }
    }

    #region getters/setters

    public void SetMaxValue(int value) {
        
        _maxValue = value;
    }

    public void SetCurrentValue(int value) {

        _currentValue = value;
    }

    public void SetValue(int current, int max) {
        
        SetCurrentValue(current);
        SetMaxValue(max);
    }

    public bool GetIsOver() {
        
        CheckStatus();

        return _isOnFinishValue;
    }
    
    #endregion
}
