using HCExample.Generators.PlaneGrid;
using UnityEngine;

public class Level : MonoBehaviour {

	[Header("Components")] 
	[SerializeField] private CustomCamera _mainCamera;
	[SerializeField] private Player _player;

	[Header("Danger Zone")] 
	[SerializeField] private PlaneGrid _dangerZonePlaneGrid;
	
	#region getters

	public CustomCamera MainCamera => _mainCamera;

	public Player Player => _player;

	#endregion

	private void Start() {
		_mainCamera.SetFollowTarget(_player.transform);
		
		_dangerZonePlaneGrid.GenerateGrid();
	}
}
