using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour {
    
    [SerializeField] private bool isInvincible = false; 
    
    [SerializeField] private int maxHealth = 3;
	[SerializeField] private int currentHealth = 3;

	bool isDead = false;

	public event Action HPChanged;

	private void Start() {
		CheckStatus();
	}

	public void SetMaxHealth(int _value) {
		maxHealth = _value;
	}

	public void SetCurrentHealth(int _value) {
        
        if (isInvincible) {
            
            Debug.Log("Hes goddamn invincible baby!");
            
            currentHealth = maxHealth;
            return;
        }
        
		currentHealth = _value;
	}

	public void Reset() {
		currentHealth = maxHealth;
        
        HPChanged?.Invoke();
	}

	public void Restore(int _count) {
        
		currentHealth += _count;

		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
        
        HPChanged?.Invoke();

		CheckStatus();
	}

	public void DealDamage(int _count) {
        
        if (isInvincible) {
            
            Debug.Log("Hes goddamn invincible baby!");
            
            HPChanged?.Invoke();
            
            return;
        }
        
		currentHealth -= _count;
	
		if (currentHealth < 0) {
			currentHealth = 0;
		}
        
        HPChanged?.Invoke();

		CheckStatus();
	}

	void CheckStatus() {
		if (currentHealth <= 0) {
			isDead = true;
		} else {
			isDead = false;
		}
    }

	#region getters/setters

	public int GetMaxValue() {
		return maxHealth;
	}

	public int GetCurrentValue() {
		return currentHealth;
	}

	public bool GetIsDead() {

		CheckStatus();

		return isDead;
	}

	#endregion
}
