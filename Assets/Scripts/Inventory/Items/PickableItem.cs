using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class PickableItem : MonoBehaviour {

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private MoveToTransform _moveToTransform;

    private bool _availableToPick = false;
    
    #region getters

    public MoveToTransform MoveToTransform => _moveToTransform;

    public bool AvailableToPick {
        get => _availableToPick;
        set => _availableToPick = value;
    }

    #endregion
    
    private InventoryItemData _itemData;

    public InventoryItemData ItemData {
        get => _itemData;
        set {
            _itemData = value;
            _spriteRenderer.sprite = _itemData.ItemSprite;
        }
    }

    public void Pick(Inventory inventory) {
        
        _moveToTransform.StartMoveToTransform(inventory.gameObject.transform, 5, () => {
            inventory.AddItem(_itemData);
            
            LeanPool.Despawn(gameObject);
        });

        _availableToPick = false;
    }

    private void OnDisable() {
        _availableToPick = false;
    }
}
