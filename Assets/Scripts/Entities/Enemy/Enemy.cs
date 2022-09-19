using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Enemy : Entity
{
	public enum ENEMY_ACTION_MODE { IDLE, MOVE_AROUND, CHASE_PLAYER }

	[Header("Components")] 
	[SerializeField] private EnemyCollisions _collisions;
	[SerializeField] private EnemyAI _ai;
	[SerializeField] private Weapon _weapon;
	[SerializeField] private LootSpawner _lootSpawner;
	
	[Header("Options")]
	[SerializeField] private ENEMY_ACTION_MODE _actionMode;
	
	#region getters
	public EnemyAI AI => _ai;
	public Weapon Weapon => _weapon;

	public ENEMY_ACTION_MODE ActionMode {
		get => _actionMode;
		set {
			if (_actionMode != value) {
				_actionMode = value;
			}
		}
	}

	#endregion
	
	private bool _isDead = false;
	
	private void OnEnable() {
		
		_health.Reset();
		
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
		
		_lootSpawner.SpawnLoot();
		
		LeanPool.Despawn(gameObject);
		
		var level = Game.Instance.World.CurrentLevel;
		level.EnemiesManager.AnalyseEnemies();
	}
}
