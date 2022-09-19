using System;
using UnityEngine;

public class Player : Entity {

	private enum PLAYER_ACTION_MODE { IDLE, MOVE, ATTACK }

	[Header("Componets")] 
	[SerializeField] private PlayerInput _input;
	[SerializeField] private Weapon _weapon;

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

	private void ZoneChanged() {
		switch (_zoneObserver.ZoneType) {
			case World.ZONE_TYPE.PLAYER_BASE:

				_weapon.AttackEnabled = false;
				
				break;
			
			case World.ZONE_TYPE.DANGER:

				_weapon.AttackEnabled = true;
				
				break;
		}
	}
}
