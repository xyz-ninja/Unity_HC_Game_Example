using System;
using UnityEngine;

public class Player : Entity {

	private enum PLAYER_ACTION_MODE { IDLE, MOVE, ATTACK }

	[Header("Componets")] 
	[SerializeField] private PlayerInput _input;
	[SerializeField] private Weapon _weapon;
}
