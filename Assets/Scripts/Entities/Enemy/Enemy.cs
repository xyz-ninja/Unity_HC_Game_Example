using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Enemy : Entity
{
	private enum ENEMY_ACTION_MODE { IDLE, MOVE_AROUND, CHASE_PLAYER }

	private bool _isDead = false;
	
	private void OnEnable() {
		_health.HPChanged += HPChanged;
		
		_isDead = false;
	}

	protected override void OnDisable() {

		_health.HPChanged -= HPChanged;
		
		base.OnDisable();
	}

	private void HPChanged() {
		
		if (_health.GetIsDead()) {
			if (_isDead) {
				return;
			}
			
			Die();
		}
	}
	
	private void Die() {

		_isDead = true;
		
		LeanPool.Despawn(gameObject);
	}
}
