using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	[Header("Components")] 
	[SerializeField] private CustomCamera _mainCamera;

	#region getters

	public CustomCamera MainCamera => _mainCamera;

	#endregion
}
