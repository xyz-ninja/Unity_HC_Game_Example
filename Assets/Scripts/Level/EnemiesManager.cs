using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HCExample.Generators.PlaneGrid;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {
    
    [SerializeField] private Level _level;

    [Header("Options")] 
    [SerializeField] private int _minEnemiesSpawnCount = 8;
    [SerializeField] private int _maxEnemiesSpawnCount = 12;
    [SerializeField] private Vector3 _enemySpawnOffset;
    
    private List<Enemy> _enemies = new List<Enemy>();

    #region getters
    
    public int EnemiesCount => _enemies.Count;

    #endregion
    
    public void SpawnEnemiesOnGrid(PlaneGrid grid) {

        int spawnCount = Random.Range(_minEnemiesSpawnCount, _maxEnemiesSpawnCount + 1);

        var availableCells = grid.NotLockedCells.Clone();

        for (int i = 0; i < spawnCount; i++) {

            var selectedIndex = Random.Range(0, availableCells.Count);
            
            var cell = availableCells[selectedIndex];
            var targetPosition = cell.WorldPosition + _enemySpawnOffset;

            availableCells.RemoveAt(selectedIndex);
            
            var enemyObject = PrefabsCreator.CreatePooledPrefab(
                PrefabsCreator.Instance.EnemyPrefab, targetPosition, _level.EnemiesT);

            var enemy = enemyObject.GetComponent<Enemy>();
            _enemies.Add(enemy);
        }
    }

    public void AnalyseEnemies() {
        _enemies = _enemies.Where(enemy => enemy != null && enemy.gameObject.activeSelf).ToList();
    }
}
