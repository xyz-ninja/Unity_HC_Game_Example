using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour {
    
    [SerializeField] private bool _isInvincible = false; 
    
    [SerializeField] private int _maxHealth = 3;
	[SerializeField] private int _currentHealth = 3;

	bool _isDead = false;

	public event Action HPChanged;

	private void Start() {
		CheckStatus();
	}

	public void SetMaxHealth(int value) {
		_maxHealth = value;
	}

	public void SetCurrentHealth(int value) {
        
        if (_isInvincible) {
            
            Debug.Log("Hes goddamn invincible baby!");
            
            _currentHealth = _maxHealth;
            return;
        }
        
		_currentHealth = value;
	}

	public void Reset() {
		_currentHealth = _maxHealth;
        
        HPChanged?.Invoke();
	}

	public void Restore(int count) {
        
		_currentHealth += count;

		if (_currentHealth > _maxHealth) {
			_currentHealth = _maxHealth;
		}
        
        HPChanged?.Invoke();

		CheckStatus();
	}

	public void DealDamage(int count) {
        
        if (_isInvincible) {
            
            Debug.Log("Hes goddamn invincible baby!");
            
            HPChanged?.Invoke();
            
            return;
        }
        
		_currentHealth -= count;
	
		if (_currentHealth < 0) {
			_currentHealth = 0;
		}
        
        HPChanged?.Invoke();

		CheckStatus();
	}

	void CheckStatus() {
		if (_currentHealth <= 0) {
			_isDead = true;
		} else {
			_isDead = false;
		}
    }

	#region getters/setters

	public int GetMaxValue() {
		return _maxHealth;
	}

	public int GetCurrentValue() {
		return _currentHealth;
	}

	public bool GetIsDead() {

		CheckStatus();

		return _isDead;
	}

	#endregion
}
