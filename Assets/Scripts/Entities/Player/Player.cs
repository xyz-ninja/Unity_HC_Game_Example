using System;
using UnityEngine;

public class Player : Entity {

	public enum PLAYER_ACTION_MODE { IDLE, MOVE, ATTACK }

	[Header("Componets")] 
	[SerializeField] private PlayerInput _input;
	[SerializeField] private PlayerCollisions _collisions;
	[SerializeField] private Inventory _inventory;

	[SerializeField] private Weapon _weapon;

	private PLAYER_ACTION_MODE _actionMode = PLAYER_ACTION_MODE.IDLE;

	#region getters/setters

	public PLAYER_ACTION_MODE ActionMode {
		get => _actionMode;
		set {
			if (_actionMode != value) {
				_actionMode = value;
			}
		}
	}

	public Inventory Inventory => _inventory;
	
	#endregion
	
	private void Awake() {
		_zoneObserver.ZoneChanged += ZoneChanged;
	}

	protected override void OnDisable() {
		_zoneObserver.ZoneChanged -= ZoneChanged;
		
		base.OnDisable();
	}

	private void Start() {
		_weapon.AttackEnabled = false;
	}

	private void Update() {
		UpdateCurrentActionMode();
		UpdateMode();
	}

	private void UpdateCurrentActionMode() {
		if (_weapon.AttackTargetEntity != null) {
			
			ActionMode = PLAYER_ACTION_MODE.ATTACK;
			
		} else {
			if (_movement.IsMove) {
				
				ActionMode = PLAYER_ACTION_MODE.MOVE;
				
			} else {
				
				ActionMode = PLAYER_ACTION_MODE.IDLE;
			}
		}
	}

	private void UpdateMode() {
		switch (_actionMode) {
			case PLAYER_ACTION_MODE.ATTACK:
				
				_movement.RotateToMoveDirection = false;
				
				var attackTarget = _weapon.AttackTargetEntity;
				_rootT.RotateToDirection((attackTarget.transform.position - transform.position).normalized);
				
				break;
			
			default:
				
				_movement.RotateToMoveDirection = true;
				
				break;
		}
	}
	
	private void ZoneChanged() {
		switch (_zoneObserver.ZoneType) {
			case World.ZONE_TYPE.PLAYER_BASE:

				_weapon.AttackEnabled = false;

				var level = Game.Instance.World.CurrentLevel;
				if (level.EnemiesManager.EnemiesCount == 0) {
					level.GenerateEnemies();
				}

				Debug.Log("Entered Zone : Player Base");
				
				break;
			
			case World.ZONE_TYPE.DANGER:

				_weapon.AttackEnabled = true;
				
				Debug.Log("Entered Zone : Danger");
				
				break;
		}
	}
}
