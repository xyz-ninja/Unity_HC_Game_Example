using System.Collections;
using System.Collections.Generic;
using HCExample.Generators.PlaneGrid;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {
    
    [SerializeField] private Level _level;

    [Header("Options")] 
    [SerializeField] private int _minEnemiesSpawnCount = 8;
    [SerializeField] private int _maxEnemiesSpawnCount = 12;
    [SerializeField] private Vector3 _enemySpawnOffset;
    
    public void SpawnEnemiesOnGrid(PlaneGrid grid) {

        int spawnCount = Random.Range(_minEnemiesSpawnCount, _maxEnemiesSpawnCount + 1);

        var availableCells = grid.NotLockedCells.Clone();

        for (int i = 0; i < spawnCount; i++) {

            var selectedIndex = Random.Range(0, availableCells.Count);
            
            var cell = availableCells[selectedIndex];
            var targetPosition = cell.WorldPosition + _enemySpawnOffset;

            PrefabsCreator.CreatePooledPrefab(PrefabsCreator.Instance.EnemyPrefab, targetPosition, _level.EnemiesT);

            availableCells.RemoveAt(selectedIndex);
        }
    }
}
