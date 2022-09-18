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
		
		private float _densityMultiplier = 1f;
		
		private List<Cell> _cells = new List<Cell>();

		[Button()]
		public void GenerateGrid() {
			_cells.Clear();
			
			var mesh = _meshFilter.sharedMesh;

			float extentX = mesh.bounds.extents.x;
			float extentZ = mesh.bounds.extents.z;
			
			var densityX = Mathf.RoundToInt(_cellsDensityX * _densityMultiplier);
			var densityZ = Mathf.RoundToInt(_cellsDensityZ * _densityMultiplier);

			float marginX = extentX * 2 / densityX;
			float marginZ = extentZ * 2 / densityZ;
			
			//densityX = Mathf.Clamp(densityX, minDensity, densityX);
			densityX = Mathf.Clamp(densityX, 1, densityX);
			//densityZ = Mathf.Clamp(densityZ, minDensity, densityZ);
			densityZ = Mathf.Clamp(densityZ, 1, densityZ);

			for (int row = 0; row < densityX; row++) {
				for (int col = 0; col < densityZ; col++) {
					
					var targetPosition = _planeT.TransformPoint(
						-extentX + marginX * row, 
						_planeT.position.y,
						-extentZ + marginZ * col);
					
					var cell = new Cell(new Vector2(row, col) , targetPosition);
					_cells.Add(cell);
				}
			}
		}

		private void OnDrawGizmos() {
			foreach (var cell in _cells) {
				Gizmos.color = Color.green;
				Gizmos.DrawWireCube(cell.WorldPosition, _cellSize);
			}
		}
	}
}