using UnityEngine;

namespace HCExample.Generators.PlaneGrid {

	public class Cell {
		
		private Vector2 _gridPosition;
		private Vector3 _worldPosition;

		#region getters

		public Vector2 GridPosition => _gridPosition;
		public Vector3 WorldPosition => _worldPosition;

		#endregion
		
		public Cell(Vector2 gridPosition, Vector3 worldPosition) {
			_gridPosition = gridPosition;
			_worldPosition = worldPosition;
		}
	}
}
