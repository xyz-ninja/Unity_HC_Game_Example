using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	[Header("Components")] 
	[SerializeField] private CustomCamera _mainCamera;
	[SerializeField] private Player _player;
	
	#region getters

	public CustomCamera MainCamera => _mainCamera;

	public Player Player => _player;

	#endregion

	private void Start() {
		_mainCamera.SetFollowTarget(_player.transform);
	}
}
