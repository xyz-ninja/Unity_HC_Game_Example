using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace HCExample.Generators.PlaneGrid {
	public class PlaneGrid : MonoBehaviour {

		[Header("Main")]
		[SerializeField] private Transform _planeT;
		[SerializeField] private MeshFilter _meshFilter;

		[Header("Parametres")] 
		[SerializeField] private int _cellsDensityX = 10;
		[SerializeField] private int _cellsDensityZ = 10;
		[SerializeField] private Vector3 _cellSize = Vector3.one;
		[SerializeField] private bool _hasExcludeLayers = false;
		[ShowIf("_hasExcludeLayers"), SerializeField] private LayerMask _excludeLayers;
		
		private float _densityMultiplier = 1f;
		
		private List<Cell> _cells = new List<Cell>();
		private List<Cell> _lockedCells = new List<Cell>();
		private List<Cell> _notLockedCells = new List<Cell>();
		
		[Button()]
		public void GenerateGrid() {
			_cells.Clear();
			
			var mesh = _meshFilter.sharedMesh;

			float extentX = mesh.bounds.extents.x;
			float extentZ = mesh.bounds.extents.z;
			
			var densityX = Mathf.RoundToInt(_cellsDensityX * _densityMultiplier);
			var densityZ = Mathf.RoundToInt(_cellsDensityZ * _densityMultiplier);

			densityX = Mathf.Clamp(densityX, 1, densityX);
			densityZ = Mathf.Clamp(densityZ, 1, densityZ);
			
			float marginX = extentX * 2 / densityX;
			float marginZ = extentZ * 2 / densityZ;

			for (int row = 0; row < densityZ; row++) {
				for (int col = 0; col < densityX; col++) {
					
					var gridPosition = new Vector2(col, row);
					var worldPosition = _planeT.TransformPoint(
						-extentX + marginX * col, 
						_planeT.position.y,
						-extentZ + marginZ * row);
					
					var cell = new Cell(gridPosition, worldPosition);
					_cells.Add(cell);

					// check exlude layers
					if (_hasExcludeLayers) {
						if (Physics.Raycast(worldPosition, Vector3.up * 3, 3, _excludeLayers)) {
							cell.IsLocked = true;
						}
					}

					if (cell.IsLocked) {
						_lockedCells.Add(cell);
					} else {
						_notLockedCells.Add(cell);
					}
				}
			}
		}

		private void OnDrawGizmos() {
			
			foreach (var cell in _cells) {
				if (cell.IsLocked) {
					Gizmos.color = Color.red;
				} else {
					Gizmos.color = Color.green;
				}

				Gizmos.DrawWireCube(cell.WorldPosition, _cellSize);
			}
		}
	}
}