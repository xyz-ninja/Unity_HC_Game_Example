using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootSpawner : MonoBehaviour {

    [Header("Transforms")]
    [SerializeField] private Transform _spawnPointT;
    
    [Header("Options")]
    [SerializeField] private Vector2 _dropItemsRange = new Vector2(2, 5);
    [SerializeField] private List<InventoryItemData> _dropItems = new List<InventoryItemData>();
    [SerializeField] private Vector2 _dropRandomRange = new Vector2(0.7f, 1.5f);
    
    [Button()]
    public void SpawnLoot() {
        
        int itemsCount = (int)Random.Range(_dropItemsRange.x, _dropItemsRange.y + 1);

        for (int i = 0; i < itemsCount; i++) {

            var spawnPosition = _spawnPointT.position;
            
            var pickableItemObject =
                PrefabsCreator.CreatePooledPrefab(PrefabsCreator.Instance.PickableItemPrefab, spawnPosition);

            var pickableItem = pickableItemObject.GetComponent<PickableItem>();
            pickableItem.ItemData = _dropItems[Random.Range(0, _dropItems.Count)];
            
            var angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            var radius = Random.Range(_dropRandomRange.x, _dropRandomRange.y);
            
            var targetPoint = new Vector3(
                spawnPosition.x + Mathf.Cos(angle) * radius,
                spawnPosition.y,
                spawnPosition.z + Mathf.Sin(angle) * radius
            );
            
            pickableItem.MoveToTransform.StartMoveToPoint(targetPoint, 5, () => {
                pickableItem.AvailableToPick = true;
            });
        }
    }
}
