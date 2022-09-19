using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthProgressbar : Progressbar {
    
    [Header("Health Progressbar")]
    [SerializeField] private Health _health;

    [SerializeField] private bool _hideWhenNoDamage = true;
    [SerializeField] private float _showDuration = 1.5f;
    private Timer _showTimer;

    private bool _isVisible = true;

    private void Awake() {
        _health.HPChanged += UpdateHealthView;
        
        _showTimer = new Timer(_showDuration);
    }

    private void Start() {
        
        if (_hideWhenNoDamage) {
            _isVisible = false;
        }

        _rootT.gameObject.SetActive(_isVisible);
    }

    private void OnDestroy() {
        _health.HPChanged -= UpdateHealthView;
    }

    private void Update() {
        if (_isVisible && _hideWhenNoDamage) {
            _showTimer.Update(Time.deltaTime);

            if (_showTimer.IsFinish()) {
                _isVisible = false;
                
                _rootT.gameObject.SetActive(false);
                
                _showTimer.Reload();
            }
        }
    }

    private void UpdateHealthView() {
        SetValues(_health.GetMaxValue(), _health.GetCurrentValue());
        
        _isVisible = true;
        
        _rootT.gameObject.SetActive(true);
        
        UpdateView();
    }
}
