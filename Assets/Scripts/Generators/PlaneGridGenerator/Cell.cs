using UnityEngine;

namespace HCExample.Generators.PlaneGrid {

	public class Cell {
		
		private Vector2 _gridPosition;
		private Vector3 _worldPosition;

		private bool _isLocked = false;
		
		#region getters/setters

		public Vector2 GridPosition => _gridPosition;
		public Vector3 WorldPosition => _worldPosition;

		public bool IsLocked {
			get => _isLocked;
			set => _isLocked = value;
		}

		#endregion
		
		public Cell(Vector2 gridPosition, Vector3 worldPosition) {
			_gridPosition = gridPosition;
			_worldPosition = worldPosition;
		}
	}
}
