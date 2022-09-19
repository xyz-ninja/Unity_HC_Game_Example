using HCExample.Generators.PlaneGrid;
using UnityEngine;

public class Level : MonoBehaviour {

	[Header("Transforms")] 
	[SerializeField] private Transform _enemiesT;
	[SerializeField] private Transform _propsT;
	
	[Header("Components")] 
	[SerializeField] private CustomCamera _mainCamera;
	[SerializeField] private Player _player;

	[Header("Managers")] 
	[SerializeField] private EnemiesManager _enemiesManager;
	
	[Header("Generators")] 
	[SerializeField] private PlaneGrid _dangerZonePlaneGrid;

	private bool _isWin = false;
	private bool _isLose = false;
	
	#region getters

	public Transform EnemiesT => _enemiesT;

	public CustomCamera MainCamera => _mainCamera;
	public Player Player => _player;
	public PlaneGrid DangerZonePlaneGrid => _dangerZonePlaneGrid;

	public EnemiesManager EnemiesManager => _enemiesManager;

	#endregion

	private void Start() {
		_mainCamera.SetFollowTarget(_player.transform);
		
		_dangerZonePlaneGrid.GenerateGrid();

		Time.timeScale = 1f;
		
		GenerateEnemies();
	}

	public void GenerateEnemies() {
		_enemiesManager.SpawnEnemiesOnGrid(_dangerZonePlaneGrid);
	}

	public void Win() {
		
	}

	public void Lose() {
		if (_isLose) {
			return;
		}

		Time.timeScale = 0.2f;
		GUI.Instance.PanelsManager.GameOverPanel.Open();
		
		_isLose = true;
	}
}
